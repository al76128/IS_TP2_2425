using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producerr
{
    public class Produto
    {
        public string Codigo_Peca { get; set; }
        public DateTime Data_Producao { get; set; }
        public TimeSpan Hora_Producao { get; set; }
        public int Tempo_Producao { get; set; }
        public string Codigo_Resultado { get; set; }

        private static readonly Random random = new Random();

        public static Produto GerarProdutoAleatorio()
        {
            string[] tiposProduto = { "aa", "ab", "ba", "bb" };
            string tipo = tiposProduto[random.Next(tiposProduto.Length)];
            string identificador = Guid.NewGuid().ToString("N").Substring(0, 6);
            string codigoPeca = tipo + identificador;

            DateTime dataProducao = DateTime.Now.Date;
            TimeSpan horaProducao = DateTime.Now.TimeOfDay;
            int tempoProducao = random.Next(10, 51);

            // ✅ Usa distribuição realista para o resultado do teste
            string codigoResultado = GerarCodigoResultado();

            return new Produto
            {
                Codigo_Peca = codigoPeca,
                Data_Producao = dataProducao,
                Hora_Producao = horaProducao,
                Tempo_Producao = tempoProducao,
                Codigo_Resultado = codigoResultado
            };
        }

        // ✅ Método ativo para gerar resultado com distribuição realista
        private static string GerarCodigoResultado()
        {
            int chance = random.Next(100); // Número entre 0 e 99

            if (chance < 70)
                return "01"; // 70% OK
            else if (chance < 75)
                return "02"; // 5% inspeção visual
            else if (chance < 80)
                return "03"; // 5% resistência
            else if (chance < 85)
                return "04"; // 5% dimensões
            else if (chance < 90)
                return "05"; // 5% estanqueidade
            else
                return "06"; // 5% desconhecido
        }
    }
}
