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
        public float PrecoUnitario { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public float PrecoLucro { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public float Roic { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public float EvEbit { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public float MargemEbit { get; set; }

        public float? DividendYield { get; set; }
        public double? PrecoValorPatrimonial { get; set; }
        public double? LiquidezMediaDiaria { get; set; }
        public double? ValorMercado { get; set; }
    }
}
