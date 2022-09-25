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

        private decimal _precoUnitario;
        public decimal PrecoUnitario
        {
            get { return _precoUnitario; }
            set { _precoUnitario = value; }
        }

        private decimal _precoLucro;
        public decimal PrecoLucro
        {
            get { return _precoLucro; }
            set { _precoLucro = value; }
        }

        private decimal _roic;
        public decimal Roic
        {
            get { return _roic; }
            set { _roic = value; }
        }

        private decimal _evEbit;
        public decimal EvEbit
        {
            get { return _evEbit; }
            set { _evEbit = value; }
        }

        private decimal _margemEbit;
        public decimal MargemEbit
        {
            get { return _margemEbit; }
            set { _margemEbit = value; }
        }

        private decimal? _dividendYield;
        public decimal? DividendYield
        {
            get { return _dividendYield; }
            set { _dividendYield = (value == 0 ? null : value); }
        }

        private decimal? _precoValorPatrimonial;
        public decimal? PrecoValorPatrimonial
        {
            get { return _precoValorPatrimonial; }
            set { _precoValorPatrimonial = (value == 0 ? null : value); }
        }

        private decimal? _liquidezMediaDiaria;
        public decimal? LiquidezMediaDiaria
        {
            get { return _liquidezMediaDiaria; }
            set { _liquidezMediaDiaria = (value == 0 ? null : value); }
        }

        private decimal? _valorMercado;
        public decimal? ValorMercado
        {
            get { return _valorMercado; }
            set { _valorMercado = (value == 0 ? null : value); }
        }
    }
}
