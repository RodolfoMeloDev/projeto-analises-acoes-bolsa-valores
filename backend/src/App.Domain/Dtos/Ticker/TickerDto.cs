using System;
using App.Domain.Enums;

namespace App.Domain.Dtos.Ticker
{
    public class TickerDto : BaseDto
    {
        public string BaseTicker { get; set; }
        public string Ticker { get; set; }

        private TypeTicker _typeTicker;
        public TypeTicker TypeTicker
        {
            get { return _typeTicker; }
            set { _typeTicker = value; }
        }

        public string NameType
        {
            get { return Enum.GetName(typeof(TypeTicker), _typeTicker); }
        }

        public bool JudicialRecovery { get; set; }
        public int SegmentId { get; set; }
    }
}
