using System.ComponentModel.DataAnnotations;

namespace App.Domain.Dtos.HistoryTicker
{
    public class HistoryTickerDtoCreate
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        public int ArquivoImportacaoId { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public int TickerId { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal PrecoUnitario { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal PrecoLucro { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal Roic { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal EvEbit { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal MargemEbit { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal Lpa { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal Vpa { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal Roe { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal CrescimentoEsperado { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
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
