namespace App.Domain.Dtos.Formula
{
    public class ParametersFilter
    {
        public int FileImportId { get; set; }
        public bool RemoveItemsJudicialRecovery { get; set; }
        public bool RemoveLowerLiquidity { get; set; }
        public bool RemoveItemsWithZeroValue { get; set; }
        public bool RemoveItemsWithNegativeValue { get; set; }
        public decimal? MinimumLiquidity { get; set; }
        public decimal? MinimunEvEbit { get; set; }
        public decimal? MaximumEvEbit { get; set; }
        public decimal? MinimumPriceByProfit { get; set; }
        public decimal? MaximumPriceByProfit { get; set; }
        public decimal? MinimumEbitMargem { get; set; }
        public decimal? MaximumEbitMargem { get; set; }
        public decimal? MarketRisk { get; set; }
        public string Ticker { get; set; }
    }
}
