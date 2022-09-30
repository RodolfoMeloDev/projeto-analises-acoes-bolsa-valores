namespace App.Domain.Dtos.HistoryTicker
{
    public class HistoryTickerDto : BaseDto
    {
        public int FileImportId { get; set; }
        public int TickerId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal PriceByProfit { get; set; }
        public decimal Roic { get; set; }
        public decimal EvEbit { get; set; }
        public decimal EbitMargin { get; set; }
        public decimal Lpa { get; set; }
        public decimal Vpa { get; set; }
        public decimal Roe { get; set; }
        public decimal ExpectedGrowth { get; set; }
        public decimal AverageGrowth { get; set; }
        public decimal? DividendYield { get; set; }
        public decimal? Pvp { get; set; }
        public decimal? Dpa { get; set; }
        public decimal? Payout { get; set; }
        public decimal? ProfitCAGR { get; set; }
        public decimal? AverageDailyLiquidity { get; set; }
        public decimal? MarketValue { get; set; }
    }
}
