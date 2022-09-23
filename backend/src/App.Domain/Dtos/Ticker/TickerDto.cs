using System;
using App.Domain.Enums;

namespace App.Domain.Dtos.Ticker
{
    public class TickerDto : BaseDto
    {
        public string BaseTicker { get; set; }
        public string Ticker { get; set; }

        private TypeTicker _tipo;
        public TypeTicker Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public string NomeTipo
        {
            get { return Enum.GetName(typeof(TypeTicker), _tipo); }
        }

        public bool RecuperacaoJudicial { get; set; }
        public int SegmentoId { get; set; }
    }
}
