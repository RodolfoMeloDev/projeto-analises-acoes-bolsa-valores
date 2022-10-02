using App.Domain.Dtos.BaseTicker;
using App.Domain.Enums;

namespace App.Domain.Dtos.Ticker
{
    public class TickerDtoComplete : BaseDto
    {
        public string Ticker { get; set; }
        public int BaseTickerId { get; set; }
        public BaseTickerDtoComplete BaseTicker { get; set; }
        public TypeTicker TypeTicker { get; set; }
        public bool JudicialRecovery { get; set; }
    }
}
