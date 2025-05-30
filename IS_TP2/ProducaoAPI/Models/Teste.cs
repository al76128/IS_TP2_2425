using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProducaoAPI.Models
{
    public class Teste
    {
            [Key]
            public int ID_Teste { get; set; }

            [Required]
            [StringLength(2)]
            [RegularExpression("0[1-6]")] // Valida códigos 01-06
            public string Codigo_Resultado { get; set; }

            public DateTime? DataTeste { get; set; } = DateTime.Now;

    }
}
