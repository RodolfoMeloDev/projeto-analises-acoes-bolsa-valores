using System;
using App.Domain.Enums;

namespace App.Domain.Dtos.Ticker
{
    public class TickerDtoCreateResult : BaseCreateDtoResult
    {
        public string Ticker { get; set; }
        public int BaseTickerId { get; set; }
        
        private TypeTicker _typeTicker;
        public TypeTicker TypeTicker
        {
            get { return _typeTicker; }
            set { _typeTicker = value; }
        }

        public string NomeTipo
        {
            get { return Enum.GetName(typeof(TypeTicker), _typeTicker); }
        }

        public bool JudicialRecovery { get; set; }
    }
}
