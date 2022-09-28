using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public class HistoryTickerEntity : BaseEntity
    {
        [Required]
        public int ArquivoImportacaoId { get; set; }

        public FileImportEntity ArquivoImportacao { get; set; }

        [Required]
        public int TickerId { get; set; }

        public TickerEntity Ticker { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal PrecoUnitario { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal PrecoLucro { get; set; }

        [Required]
        public decimal Roic { get; set; }

        [Required]
        public decimal EvEbit { get; set; }

        [Required]
        public decimal MargemEbit { get; set; }

        [Required]
        public decimal Lpa { get; set; }

        [Required]
        public decimal Roe { get; set; }

        [Required]
        public decimal CrescimentoEsperado { get; set; }

        [Required]
        public decimal MediaCrescimento { get; set; }

        public decimal? DividendYield { get; set; }
        public decimal? PrecoValorPatrimonial { get; set; }
        public decimal? Dpa { get; set; }
        public decimal? Payout { get; set; }
        public decimal? CAGRLucro { get; set; }
        public decimal? LiquidezMediaDiaria { get; set; }
        public decimal? ValorMercado { get; set; }
    }
}
