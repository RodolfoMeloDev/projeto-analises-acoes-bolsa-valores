namespace App.Domain.Dtos.HistoryTicker
{
    public class HistoryTickerDtoCreateResult : BaseCreateDtoResult
    {
        public int ArquivoImportacaoId { get; set; }
        public int TickerId { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoLucro { get; set; }
        public decimal Roic { get; set; }
        public decimal EvEbit { get; set; }
        public decimal MargemEbit { get; set; }
        public decimal Lpa { get; set; }
        public decimal Roe { get; set; }
        public decimal CrescimentoEsperado { get; set; }
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
