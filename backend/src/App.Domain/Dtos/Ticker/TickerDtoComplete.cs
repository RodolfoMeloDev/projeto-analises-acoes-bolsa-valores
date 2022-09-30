using System;
using App.Domain.Dtos.Segment;
using App.Domain.Enums;

namespace App.Domain.Dtos.Ticker
{
    public class TickerDtoComplete : BaseDto
    {
        public string BaseTicker { get; set; }
        public string Ticker { get; set; }
        public string Company { get; set; }
        public string CNPJ { get; set; }
        public TypeTicker TypeTicker { get; set; }
        public bool JudicialRecovery { get; set; }
        public int SegmentId { get; set; }
        public SegmentDtoComplete Segment { get; set; }
    }
}
