using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProducaoAPI.Models;
using System.Data;

namespace ProducaoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestesController : ControllerBase
    {
        //Conexao com a base de dados
        string sqlConnectionString = "Data Source=localhost\\MEIBI2025;Initial Catalog=Producao;Integrated Security=True;Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // GET: api/Testes/
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                Console.WriteLine($"GET Request - Obter Teste ID: {id}");

                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetTestePorId", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Teste", id);

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            var teste = new
                            {
                                ID_Teste = Convert.ToInt32(reader["ID_Teste"]),
                                ID_Produto = Convert.ToInt32(reader["ID_Produto"]),
                                Codigo_Resultado = Convert.ToString(reader["Codigo_Resultado"]),
                                Descricao_Resultado = ObterDescricaoResultado(Convert.ToString(reader["Codigo_Resultado"])),
                                Data_Teste = Convert.ToDateTime(reader["Data_Teste"])
                            };

                            con.Close();
                            return Ok(teste);
                        }
                        else
                        {
                            con.Close();
                            return NotFound(new { message = "Teste não encontrado." });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro SQL ao obter teste: {ex.Message}");
                return BadRequest(new { message = "Erro ao obter teste.", sqlError = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return StatusCode(500, new { message = "Erro interno no servidor.", error = ex.Message });
            }
        }

        //Nao há posts de testes uma vez que eles vao ser inseridos diretamente apos cada inserçao de produto !!!
        //// POST: api/Testes
        //[HttpPost]
        //public ActionResult Post([FromBody] Teste teste)
        //{
        //    try
        //    {
        //        Console.WriteLine("POST Request - Inserir/Atualizar Teste");

        //        using (SqlConnection con = new SqlConnection(sqlConnectionString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("sp_RegistrarTeste", con))
        //            {
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //                cmd.Parameters.AddWithValue("@ID_Produto", teste.ID_Produto);
        //                cmd.Parameters.AddWithValue("@Codigo_Resultado", teste.Codigo_Resultado);

        //                // Parâmetro de saída para o ID do teste
        //                SqlParameter outputParam = new SqlParameter("@Novo_ID", SqlDbType.Int);
        //                outputParam.Direction = ParameterDirection.Output;
        //                cmd.Parameters.Add(outputParam);

        //                con.Open();
        //                cmd.ExecuteNonQuery();

        //                int novoId = (int)outputParam.Value;
        //                con.Close();

        //                return CreatedAtAction(nameof(Get), new { id = novoId }, new
        //                {
        //                    message = "Teste registrado com sucesso!",
        //                    testeId = novoId
        //                });
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine($"Erro SQL ao registrar teste: {ex.Message}");
        //        return BadRequest(new
        //        {
        //            message = "Erro ao registrar teste.",
        //            sqlError = ex.Message
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Erro inesperado: {ex.Message}");
        //        return StatusCode(500, new
        //        {
        //            message = "Erro interno no servidor.",
        //            error = ex.Message
        //        });
        //    }
        //}

        // PUT: api/Testes/
        [HttpPut]
        public ActionResult Put([FromBody] Teste teste)
        {
            try
            {
                Console.WriteLine($"PUT Request - Atualizar Teste");

                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateTeste", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Teste", teste.ID_Teste);
                        cmd.Parameters.AddWithValue("@Codigo_Resultado", teste.Codigo_Resultado);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        
                        return Ok(new { message = "Teste atualizado com sucesso!" });
                                               
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar teste: {ex.Message}");
                return BadRequest(new { message = "Erro ao atualizar teste.", sqlError = ex.Message });
            }
        }

        // DELETE: api/Testes/
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                Console.WriteLine($"DELETE Request - Teste ID: {id}");

                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteTeste", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Teste", id);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                            return Ok(new { message = "Teste eliminado com sucesso!" });
                        else
                            return NotFound(new { message = "Teste não encontrado." });
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Erro SQL ao eliminar teste: {ex.Message}");
                return BadRequest(new { message = "Erro ao eliminar teste.", sqlError = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return StatusCode(500, new { message = "Erro interno no servidor.", error = ex.Message });
            }
        }

        //Funcao para dizer a que se deve o codigo_resultado (Protocolo do trabalho)
        private string ObterDescricaoResultado(string codigoResultado)
        {
            return codigoResultado switch
            {
                "01" => "Ok",
                "02" => "Falha na inspeção visual",
                "03" => "Falha na inspeção de resistência",
                "04" => "Falha na inspeção de dimensões",
                "05" => "Falha na inspeção de estanqueidade",
                "06" => "Desconhecido",
                _ => "Código inválido"
            };
        }
    }

}
