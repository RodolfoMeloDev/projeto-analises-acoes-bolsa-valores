using App.Domain.Enums;

namespace App.Domain.Models
{
    public class TickerModel : BaseModel
    {
        private string _baseTicker;
        public string BaseTicker
        {
            get { return _baseTicker; }
            set { _baseTicker = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private string _ticker;
        public string Ticker
        {
            get { return _ticker; }
            set { _ticker = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private string _company;
        public string Company
        {
            get { return _company; }
            set { _company = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private string _cnpj;
        public string CNPJ
        {
            get { return _cnpj; }
            set { _cnpj = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private TypeTicker _typeTicker;
        public TypeTicker TypeTicker
        {
            get { return _typeTicker; }
            set { _typeTicker = value; }
        }

        private bool _judicialRecovery;
        public bool JudicialRecovery
        {
            get { return _judicialRecovery; }
            set { _judicialRecovery = value; }
        }

        private int? _segmentId;
        public int? SegmentId
        {
            get { return _segmentId; }
            set { _segmentId = value; }
        }
    }
}
