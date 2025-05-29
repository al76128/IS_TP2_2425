using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;


var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.ExchangeDeclareAsync(exchange: "producao_exchange", type: ExchangeType.Topic);

// declara fila temporária
var queueDeclareResult = await channel.QueueDeclareAsync();
string queueName = queueDeclareResult.QueueName;

// faz bind ao exchange
await channel.QueueBindAsync(queue: queueName, exchange: "producao_exchange", routingKey: "dados.producao.#");

Console.WriteLine(" [*] À espera de mensagens da produção...");

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var json = Encoding.UTF8.GetString(body);
    var routingKey = ea.RoutingKey;

    try
    {
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        string codigo = root.GetProperty("codigo_peca").GetString();
        int tempo = root.GetProperty("tempo_producao").GetInt32();
        string resultado = root.GetProperty("resultado_teste").GetString();
        string dataStr = root.GetProperty("data").GetString();
        string horaStr = root.GetProperty("hora").GetString();

        DateTime data = DateTime.ParseExact(dataStr, "yyyy-MM-dd", null);
        TimeSpan hora = TimeSpan.ParseExact(horaStr, "hh\\:mm\\:ss", null);

        await EnviarParaApiAsync(codigo, data, hora, tempo, resultado);

        if (resultado != "01")
        {
            Console.WriteLine("!!! PEÇA COM FALHA:");
            Console.WriteLine($"→ Código: {codigo}");
            Console.WriteLine($"→ Tempo: {tempo} segundos");
            Console.WriteLine($"→ Resultado: {resultado}");
            Console.WriteLine($"→ Produzido em: {data:dd/MM/yyyy} {hora}");
            Console.WriteLine(new string('-', 40));
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao processar mensagem: " + ex.Message);
    }

    await Task.CompletedTask;
};

await channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: consumer);
Console.WriteLine(" Pressiona Enter para sair.");
Console.ReadLine();


// 👉 Método que envia os dados para a API REST em vez de SQL
static async Task EnviarParaApiAsync(string codigoPeca, DateTime data, TimeSpan hora, int tempo, string resultado)
{
    var produto = new
    {
        Codigo_Peca = codigoPeca,
        Data_Producao = data.ToString("yyyy-MM-dd"),
        Hora_Producao = hora.ToString(@"hh\:mm\:ss"),
        Tempo_Producao = tempo
    };

    using var client = new HttpClient();

    try
    {
        var response = await client.PostAsync(
            "http://localhost:5077/api/produto",
            new StringContent(JsonSerializer.Serialize(produto), Encoding.UTF8, "application/json")
        );

        response.EnsureSuccessStatusCode();

        Console.WriteLine("+++ Produto enviado para a API com sucesso.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("--- Erro ao enviar para API: " + ex.Message);
    }
}
