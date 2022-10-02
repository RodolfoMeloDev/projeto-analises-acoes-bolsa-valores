namespace App.Domain.Dtos.Formula
{
    public class FormulaDto
    {
        public string NameSeguiment { get; set; }
        public string Ticker { get; set; }
        public decimal Price { get; set; }
        public decimal DividendYield { get; set; }
        public decimal PriceByProfit { get; set; }
        public decimal Lpa { get; set; }
        public decimal Vpa { get; set; }
        public decimal? Dpa { get; set; }
        public decimal? Payout { get; set; }
        public decimal Roe { get; set; }
        public decimal Roic { get; set; }
        public decimal EvEbit { get; set; }
        public decimal EbitMargin { get; set; }
        public decimal? ProfitCAGR { get; set; }
        public decimal ExpectedGrowth { get; set; }
        public decimal AverageGrowth { get; set; }
        public decimal AverageDailyLiquidity { get; set; }
        public bool JudicialRecovery { get; set; }
    }
}
