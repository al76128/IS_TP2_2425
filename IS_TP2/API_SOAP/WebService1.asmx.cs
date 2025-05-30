using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

namespace API_SOAP
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class WebService1 : System.Web.Services.WebService
    {
        string connectionString = "Data Source=AMORIM\\MEIBI2025;Initial Catalog=Contabilidade;Integrated Security=True;";

        [WebMethod]
        public string GetPecaMaiorPrejuizo()
        {
            string query = @"SELECT Codigo_Peca, Prejuizo 
                     FROM Custos_Peca 
                     WHERE Prejuizo = (SELECT MAX(Prejuizo) FROM Custos_Peca)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                    return "Nenhuma peça encontrada.";

                StringBuilder sb = new StringBuilder();
                while (reader.Read())
                {
                    sb.AppendLine($"Peça: {reader["Codigo_Peca"]} -> Prejuízo: €{reader["Prejuizo"]}|");
                }

                return sb.ToString();
            }
        }

        [WebMethod]
        public decimal ObterCustosTotaisPorPeriodo(DateTime inicio, DateTime fim)
        {
            string query = @"SELECT SUM(Custo_Producao) 
                             FROM Custos_Peca 
                             WHERE ID_Produto IN (
                                SELECT ID_Produto 
                                FROM Producao.dbo.Produto 
                                WHERE Data_Producao BETWEEN @inicio AND @fim)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@inicio", inicio);
                cmd.Parameters.AddWithValue("@fim", fim);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }

        [WebMethod]
        public decimal ObterLucroTotalPorPeriodo(DateTime inicio, DateTime fim)
        {
            string query = @"SELECT SUM(Lucro) 
                             FROM Custos_Peca 
                             WHERE ID_Produto IN (
                                SELECT ID_Produto 
                                FROM Producao.dbo.Produto 
                                WHERE Data_Producao BETWEEN @inicio AND @fim)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@inicio", inicio);
                cmd.Parameters.AddWithValue("@fim", fim);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }

        [WebMethod]
        public List<string> ObterPrejuizoTotalPorPeca(DateTime inicio, DateTime fim)
        {
            var resultado = new List<string>();

            string query = @"SELECT Codigo_Peca, SUM(Prejuizo) AS Total
                             FROM Custos_Peca
                             WHERE ID_Produto IN (
                                 SELECT ID_Produto 
                                 FROM Producao.dbo.Produto 
                                 WHERE Data_Producao BETWEEN @inicio AND @fim)
                             GROUP BY Codigo_Peca";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@inicio", inicio);
                cmd.Parameters.AddWithValue("@fim", fim);

                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string linha = $"Peça: {reader.GetString(0)} -> Prejuízo: €{reader.GetDecimal(1)}";
                    resultado.Add(linha);
                }
            }

            return resultado;
        }

        [WebMethod]
        public string ObterDadosFinanceirosPorPeca(string codigoPeca)
        {
            string query = @"SELECT Codigo_Peca, Tempo_Producao, Custo_Producao, Prejuizo, Lucro 
                             FROM Custos_Peca WHERE Codigo_Peca = @codigo";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@codigo", codigoPeca);
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return $"Peça: {reader["Codigo_Peca"]}|Tempo: {reader["Tempo_Producao"]}s|Custo: EUR {reader["Custo_Producao"]}|Prejuízo: EUR {reader["Prejuizo"]}|Lucro: EUR {reader["Lucro"]}";
                }
            }

            return "Peça não encontrada.";
        }
    }
}
