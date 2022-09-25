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

        public decimal? DividendYield { get; set; }
        public decimal? PrecoValorPatrimonial { get; set; }
        public decimal? LiquidezMediaDiaria { get; set; }
        public decimal? ValorMercado { get; set; }
    }
}
