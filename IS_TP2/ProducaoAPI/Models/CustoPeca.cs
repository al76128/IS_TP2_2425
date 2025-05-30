using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProducaoAPI.Models
{
    public class CustoPeca
    {

        [Key]
        public int ID_Custo { get; set; }

        [Required]
        [ForeignKey("Produto")]
        public int ID_Produto { get; set; }

        [Required]
        [StringLength(8)]
        public string Codigo_Peca { get; set; }

        [Required]
        public int Tempo_Producao { get; set; }

        [Required]
        public decimal Custo_Producao { get; set; }

        [Required]
        public decimal Prejuizo { get; set; }

        [Required]
        public decimal Lucro { get; set; }


    }
}
