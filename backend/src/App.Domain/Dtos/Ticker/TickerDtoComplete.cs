using System;
using App.Domain.Dtos.Segment;
using App.Domain.Enums;

namespace App.Domain.Dtos.Ticker
{
    public class TickerDtoComplete : BaseDto
    {
        public string BaseTicker { get; set; }
        public string Ticker { get; set; }
        public string Empresa { get; set; }
        public string CNPJ { get; set; }
        public TypeTicker Tipo { get; set; }
        public bool RecuperacaoJudicial { get; set; }
        public int SegmentoId { get; set; }
        public SegmentDtoComplete Segmento { get; set; }
    }
}
