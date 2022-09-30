using System;

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

        private decimal _expectedGrowth;
        public decimal ExpectedGrowth
        {
            get { return decimal.Round(_expectedGrowth, 2); }
            set { _expectedGrowth = value; }
        }

        private decimal _averageGrowth;
        public decimal AverageGrowth
        {
            get { return decimal.Round(_averageGrowth, 2); }
            set { _averageGrowth = value; }
        }

        public decimal? DividendYield { get; set; }
        public decimal? Pvp { get; set; }

        private decimal? _dpa;
        public decimal? Dpa
        {
            get { return (_dpa == null ? null : decimal.Round(Convert.ToDecimal(_dpa), 2)); }
            set { _dpa = value; }
        }

        private decimal? _payout;
        public decimal? Payout
        {
            get { return (_payout == null ? null : decimal.Round(Convert.ToDecimal(_payout), 2)); }
            set { _payout = value; }
        }

        public decimal? ProfitCAGR { get; set; }
        public decimal? AverageDailyLiquidity { get; set; }
        public decimal? MarketValue { get; set; }
    }
}
