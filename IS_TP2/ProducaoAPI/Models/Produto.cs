using System.ComponentModel.DataAnnotations;

namespace ProducaoAPI.Models
{
        public class Produto
        {
            [Key]
            public int ID_Produto { get; set; }

            
            [Required, StringLength(8)]
            public string Codigo_Peca { get; set; }

            [Required]
            public DateTime Data_Producao { get; set; }

            [Required]
            public TimeSpan Hora_Producao { get; set; }

            
            [Required, Range(10, 50)]
            public int Tempo_Producao { get; set; }
        }
}
