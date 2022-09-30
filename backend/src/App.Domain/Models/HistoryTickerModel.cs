namespace App.Domain.Models
{
    public class HistoryTickerModel : BaseModel
    {
        private int _fileImportId;
        public int FileImportId
        {
            get { return _fileImportId; }
            set { _fileImportId = value; }
        }

        private int _tickerId;
        public int TickerId
        {
            get { return _tickerId; }
            set { _tickerId = value; }
        }

        private decimal _unitPrice;
        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }

        private decimal _priceByProfit;
        public decimal PriceByProfit
        {
            get { return _priceByProfit; }
            set { _priceByProfit = value; }
        }

        private decimal _roic;
        public decimal Roic
        {
            get { return _roic; }
            set { _roic = value; }
        }

        private decimal _evEbit;
        public decimal EvEbit
        {
            get { return _evEbit; }
            set { _evEbit = value; }
        }

        private decimal _ebitMargin;
        public decimal EbitMargin
        {
            get { return _ebitMargin; }
            set { _ebitMargin = value; }
        }

        private decimal? _dividendYield;
        public decimal? DividendYield
        {
            get { return _dividendYield; }
            set { _dividendYield = (value == 0 ? null : value); }
        }

        private decimal? _pvp;
        public decimal? Pvp
        {
            get { return _pvp; }
            set { _pvp = (value == 0 ? null : value); }
        }

        private decimal? _averageDailyLiquidity;
        public decimal? AverageDailyLiquidity
        {
            get { return _averageDailyLiquidity; }
            set { _averageDailyLiquidity = (value == 0 ? null : value); }
        }

        private decimal? _marketValue;
        public decimal? MarketValue
        {
            get { return _marketValue; }
            set { _marketValue = (value == 0 ? null : value); }
        }

        private decimal _lpa;
        public decimal Lpa
        {
            get { return _lpa; }
            set { _lpa = value; }
        }

        private decimal _vpa;
        public decimal Vpa
        {
            get { return _vpa; }
            set { _vpa = value; }
        }

        private decimal _roe;
        public decimal Roe
        {
            get { return _roe; }
            set { _roe = value; }
        }

        private decimal? _dpa;
        public decimal? Dpa
        {
            get { return _dpa; }
            set { _dpa = (_dividendYield != null ? (decimal)_unitPrice * ((decimal)_dividendYield / 100) : null); }
        }

        private decimal? _payout;
        public decimal? Payout
        {
            get { return _payout; }
            set { _payout = (_lpa == 0 ? 0 : (_dpa != null ? ((decimal)_dpa / _lpa) * 100 : null)); }
        }

        private decimal? _profitCAGR;
        public decimal? ProfitCAGR
        {
            get { return _profitCAGR; }
            set { _profitCAGR = value; }
        }

        private decimal _expectedGrowth;
        public decimal ExpectedGrowth
        {
            get { return _expectedGrowth; }
            set { _expectedGrowth = (_payout != null ? ((100 - (decimal)_payout) * _roe) / 100 : _roe); }
        }

        private decimal _averageGrowth;
        public decimal AverageGrowth
        {
            get { return _averageGrowth; }
            set { _averageGrowth = (_profitCAGR == null ? _expectedGrowth : ((decimal)_profitCAGR + _expectedGrowth) / 2); }
        }
    }
}
