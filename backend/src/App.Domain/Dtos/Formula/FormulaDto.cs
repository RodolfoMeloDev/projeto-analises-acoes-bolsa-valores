namespace App.Domain.Dtos.Formula
{
    public class FormulaDto
    {
        public string BaseTicker { get; set; }
        public string Ticker { get; set; }
        public decimal Price { get; set; }
        public decimal DividendYield { get; set; }
        public decimal PriceByProfit { get; set; }
        public decimal EvEbit { get; set; }
        public decimal Roic { get; set; }
        public decimal EbitMargin { get; set; }
        public decimal AverageDailyLiquidity { get; set; }
        public bool JudicialRecovery { get; set; }
        public int EvEbitScore { get; set; }
        public int RoicScore { get; set; }
        public int FinalScore { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
