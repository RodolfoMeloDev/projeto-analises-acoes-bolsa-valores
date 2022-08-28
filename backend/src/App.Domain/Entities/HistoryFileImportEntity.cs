using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public class HistoryFileImportEntity : BaseEntity
    {
        [Required]
        public int ArquivoImportacaoId { get; set; }

        public FileImportEntity ArquivoImportacao { get; set; }

        [Required]
        public int TickerId { get; set; }

        public TickerEntity Ticker { get; set; }

        [Required]
        public float PrecoUnitario { get; set; }

        [Required]
        public float PrecoLucro { get; set; }

        [Required]
        public float Roic { get; set; }

        [Required]
        public float EvEbit { get; set; }

        [Required]
        public float MargemEbit { get; set; }

        public float? DividendYield { get; set; }
        public float? PrecoValorPatrimonial { get; set; }        
        public float? LiquidezMediaDiaria { get; set; }
        public float? ValorMercado { get; set; }
        public float? VolumeFinanceiro { get; set; }
    }
}