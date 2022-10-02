using App.Domain.Enums;

namespace App.Domain.Models
{
    public class TickerModel : BaseModel
    {
        private string _ticker;
        public string Ticker
        {
            get { return _ticker; }
            set { _ticker = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
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

        private int _baseTickerId;
        public int BaseTickerId
        {
            get { return _baseTickerId; }
            set { _baseTickerId = value; }
        }
        
    }
}
