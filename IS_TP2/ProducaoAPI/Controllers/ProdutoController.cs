using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static System.Reflection.Metadata.BlobBuilder;
using ProducaoAPI.Models;
using System.Text.Json;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProducaoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        //Tambem poderia ser feita, acedendo diretamente às tools e fazer a comunicação com a base de dados como o Professor ensinou
        string sqlConnectionString = "Data Source=localhost\\MEIBI2025;Initial Catalog=Producao;Integrated Security=True;Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        // GET: api/<Values>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                Console.Write("GET Request -  Visualizar todos os produtos\n");
                List<Produto> produtos = new List<Produto>();
                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetProdutos", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Produto item = new Produto();
                            item.ID_Produto = Convert.ToInt32(reader["ID_Produto"]);
                            item.Codigo_Peca = Convert.ToString(reader["Codigo_Peca"]);
                            item.Data_Producao = Convert.ToDateTime(reader["Data_producao"]);
                            item.Hora_Producao = TimeSpan.Parse(reader["Hora_Producao"].ToString());
                            item.Tempo_Producao = Convert.ToInt32(reader["Tempo_Producao"]);
                            produtos.Add(item);
                        }
                        con.Close();

                        return Ok(produtos);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }

        }


        // POST api/<Values>
        [HttpPost]
        [HttpPost]
        public ActionResult Post([FromBody] JsonElement body)
        {
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (!body.TryGetProperty("Produto", out var produtoElement))
                {
                    return BadRequest(new { message = "Objeto 'Produto' é obrigatório" });
                }

                var produto = produtoElement.Deserialize<Produto>(options);

                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InserirProduto", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Codigo_Peca", produto.Codigo_Peca);
                        cmd.Parameters.AddWithValue("@Data_Producao", produto.Data_Producao);
                        cmd.Parameters.AddWithValue("@Hora_Producao", produto.Hora_Producao);
                        cmd.Parameters.AddWithValue("@Tempo_Producao", produto.Tempo_Producao);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        Console.WriteLine($"Dados inseridos: {produto.Codigo_Peca}, {produto.Data_Producao:d}, {produto.Hora_Producao}, {produto.Tempo_Producao}");
                        return Ok(new { message = "Produto inserido com sucesso!" });
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 50000) 
            {
                Console.WriteLine($"Falha ao inserir Produto: {ex.Message}");
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro grave: {ex.Message}");
                return StatusCode(500, new { message = "Erro interno" });
            }
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            try
            {
                Console.WriteLine($"PUT Request - Atualizar Produto ID: {id}");

                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateProduto", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Produto", id);
                        cmd.Parameters.AddWithValue("@Codigo_Peca", produto.Codigo_Peca);
                        cmd.Parameters.AddWithValue("@Data_Producao", produto.Data_Producao);
                        cmd.Parameters.AddWithValue("@Hora_Producao", produto.Hora_Producao);
                        cmd.Parameters.AddWithValue("@Tempo_Producao", produto.Tempo_Producao);

                        con.Open();
                        cmd.ExecuteNonQuery(); // Mesmo que falhe, a exceção será capturada
                        con.Close();

                        return Ok(new { message = "Produto atualizado com sucesso!" });
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");

                // Retorna a mensagem de erro SQL para o cliente
                return BadRequest(new { message = "Erro ao atualizar produto.", sqlError = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                Console.WriteLine($"DELETE Request - Produto ID: {id}");

                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteProduto", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Produto", id);

                        con.Open();
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine($"Erro SQL ao eliminar produto: {ex.Message}");
                            return BadRequest(new { message = "Erro ao eliminar produto.", sqlError = ex.Message });
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }

                return Ok(new { message = "Produto eliminado com sucesso!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return StatusCode(500, new { message = "Erro interno no servidor.", error = ex.Message });
            }
        }

    }
}
