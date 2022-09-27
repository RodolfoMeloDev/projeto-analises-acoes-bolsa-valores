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

        private string _empresa;
        public string Empresa
        {
            get { return _empresa; }
            set { _empresa = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private string _cnpj;
        public string CNPJ
        {
            get { return _cnpj; }
            set { _cnpj = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private TypeTicker _tipo;
        public TypeTicker Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        private bool _recuperacaoJudicial;
        public bool RecuperacaoJudicial
        {
            get { return _recuperacaoJudicial; }
            set { _recuperacaoJudicial = value; }
        }

        private int? _segmentoId;
        public int? SegmentoId
        {
            get { return _segmentoId; }
            set { _segmentoId = value; }
        }
    }
}
