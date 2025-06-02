using Producerr;
using RabbitMQ.Client;
using RabbitMQ.Stream.Client;
using RabbitMQ.Stream.Client.Reliable;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.ExchangeDeclareAsync(exchange: "producao_exchange", type: ExchangeType.Topic);

// Stream setup
var streamName = "producao_stream";

var streamSystem = await StreamSystem.Create(new StreamSystemConfig());

if (!await streamSystem.StreamExists(streamName))
{
    await streamSystem.CreateStream(new StreamSpec(streamName)
    {
        MaxLengthBytes = 5_000_000_000
    });
    Console.WriteLine($"+++ Stream '{streamName}' criado.");
}
else
{
    Console.WriteLine($"info Stream '{streamName}' já existe.");
}

var streamProducer = await Producer.Create(new ProducerConfig(streamSystem, streamName));

Console.WriteLine("Producer ativo.");

while (true)
{
    var produto = Produto.GerarProdutoAleatorio();

    var mensagem = new
    {
        data = produto.Data_Producao.ToString("yyyy-MM-dd"),
        hora = produto.Hora_Producao.ToString(@"hh\:mm\:ss"),
        codigo_peca = produto.Codigo_Peca,
        tempo_producao = produto.Tempo_Producao,
        resultado_teste = produto.Codigo_Resultado
    };

    string routingKey = produto.Codigo_Resultado == "01" ? "dados.producao.ok" : "dados.producao.falha";
    string json = JsonSerializer.Serialize(mensagem);
    var body = Encoding.UTF8.GetBytes(json);

    // Envia para topic exchange
    await channel.BasicPublishAsync(exchange: "producao_exchange", routingKey: routingKey, body: body);
    Console.WriteLine($" Topic Exchange > '{routingKey}': {json}");

    // Envia para stream
    await streamProducer.Send(new Message(body));
    Console.WriteLine($" Stream > {json}");

    await Task.Delay(5000);
}