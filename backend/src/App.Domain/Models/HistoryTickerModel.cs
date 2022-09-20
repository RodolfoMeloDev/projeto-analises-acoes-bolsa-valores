namespace App.Domain.Models
{
    public class HistoryTickerModel : BaseModel
    {
        private int _arquivoImportacaoId;
        public int ArquivoImportacaoId
        {
            get { return _arquivoImportacaoId; }
            set { _arquivoImportacaoId = value; }
        }

        private int _tickerId;
        public int TickerId
        {
            get { return _tickerId; }
            set { _tickerId = value; }
        }
        
        private float _precoUnitario;
        public float PrecoUnitario
        {
            get { return _precoUnitario; }
            set { _precoUnitario = value; }
        }
        
        private float _precoLucro;
        public float PrecoLucro
        {
            get { return _precoLucro; }
            set { _precoLucro = value; }
        }
        
        private float _roic;
        public float Roic
        {
            get { return _roic; }
            set { _roic = value; }
        }
        
        private float _evEbit;
        public float EvEbit
        {
            get { return _evEbit; }
            set { _evEbit = value; }
        }

        private float _margemEbit;
        public float MargemEbit
        {
            get { return _margemEbit; }
            set { _margemEbit = value; }
        }

        private float? _dividendYield;
        public float? DividendYield
        {
            get { return _dividendYield; }
            set { _dividendYield = value; }
        }

         private float? _precoValorPatrimonial;
         public float? PrecoValorPatrimonial
         {
            get { return _precoValorPatrimonial; }
            set { _precoValorPatrimonial = value; }
         }

         private float _liquidezMediaDiaria;
         public float LiquidezMediaDiaria
         {
            get { return _liquidezMediaDiaria; }
            set { _liquidezMediaDiaria = value; }
         }

         private float _valorMercado;
         public float ValorMercado
         {
            get { return _valorMercado; }
            set { _valorMercado = value; }
         }

        private float _volumeFinanceiro;
        public float VolumeFinanceiro
        {
            get { return _volumeFinanceiro; }
            set { _volumeFinanceiro = value; }
        }                
    }
}