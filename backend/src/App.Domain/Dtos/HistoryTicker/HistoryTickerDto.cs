using App.Domain.Dtos.FileImport;
using App.Domain.Dtos.Ticker;

namespace App.Domain.Dtos.HistoryTicker
{
    public class HistoryTickerDto : BaseDto
    {
        public int ArquivoImportacaoId { get; set; }
        public int TickerId { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoLucro { get; set; }
        public decimal Roic { get; set; }
        public decimal EvEbit { get; set; }
        public decimal MargemEbit { get; set; }
        public decimal? DividendYield { get; set; }
        public decimal? PrecoValorPatrimonial { get; set; }
        public decimal? LiquidezMediaDiaria { get; set; }
        public decimal? ValorMercado { get; set; }
    }
}
