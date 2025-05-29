using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using RabbitMQ.Stream.Client;
using RabbitMQ.Stream.Client.Reliable;
using System.Buffers;

namespace Gestao_app_
{
    public partial class Form1 : Form
    {
        private int total = 0;
        private int ok = 0;
        private int falha = 0;
        private double somaTempo = 0;

        public Form1()
        {
            InitializeComponent();
            _ = IniciarStreamAsync(); // iniciar o stream no arranque
        }

        private async Task IniciarStreamAsync()
        {
            try
            {
                var config = new StreamSystemConfig
                {
                    UserName = "guest",
                    Password = "guest",

                };

                var streamSystem = await StreamSystem.Create(config);
                string streamName = "producao_stream";

                if (!await streamSystem.StreamExists(streamName))
                {
                    MessageBox.Show("O stream 'producao_stream' não existe.");
                    return;
                }

                await Consumer.Create(new ConsumerConfig(streamSystem, streamName)
                {
                    OffsetSpec = new OffsetTypeFirst(),
                    MessageHandler = async (stream, _, _, message) =>
                    {
                        var json = Encoding.UTF8.GetString(message.Data.Contents.ToArray());

                        try
                        {
                            using var doc = JsonDocument.Parse(json);
                            var root = doc.RootElement;

                            int tempo = root.GetProperty("tempo_producao").GetInt32();
                            string resultado = root.GetProperty("resultado_teste").GetString();

                            total++;
                            somaTempo += tempo;

                            if (resultado == "01") ok++;
                            else falha++;

                            Invoke(() =>
                            {
                                labelTotal.Text = $"Total de peças: {total}";
                                labelOK.Text = $"OK: {ok}";
                                labelFalha.Text = $"Falhas: {falha}";
                                labelMediaTempo.Text = $"Média tempo: {(somaTempo / total):0.0} s";
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao processar mensagem: " + ex.Message);
                        }

                        await Task.CompletedTask;
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao iniciar consumidor: " + ex.Message);
            }
        }
    }
}
