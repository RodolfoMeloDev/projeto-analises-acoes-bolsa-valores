using App.Domain.Enums;

namespace App.Domain.Models
{
    public class TickerModel : BaseModel
    {
        private string _baseTicker;
        public string BaseTicker
        {
            get { return _baseTicker; }
            set { _baseTicker = value; }
        }
        
        private string _ticker;
        public string Ticker
        {
            get { return _ticker  ; }
            set { _ticker  = value; }
        }
        
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        
        private string _empresa;
        public string Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        private string _cnpj;
        public string CNPJ
        {
            get { return _cnpj; }
            set { _cnpj = value; }
        }
        
        private string _descricao;
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        
        private string _site;
        public string Site
        {
            get { return _site; }
            set { _site = value; }
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

        private int _segmentoId;
        public int SegmentoId
        {
            get { return _segmentoId; }
            set { _segmentoId = value; }
        }                
    }
}