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
            set { _precoUnitario = decimal.Round(value, 2); }
        }

        private decimal _precoLucro;
        public decimal PrecoLucro
        {
            get { return _precoLucro; }
            set { _precoLucro = decimal.Round(value, 2); }
        }

        private decimal _roic;
        public decimal Roic
        {
            get { return _roic; }
            set { _roic = decimal.Round(value, 2); }
        }

        private decimal _evEbit;
        public decimal EvEbit
        {
            get { return _evEbit; }
            set { _evEbit = decimal.Round(value, 2); }
        }

        private decimal _margemEbit;
        public decimal MargemEbit
        {
            get { return _margemEbit; }
            set { _margemEbit = decimal.Round(value, 2); }
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

        private decimal _lpa;
        public decimal Lpa
        {
            get { return _lpa; }
            set { _lpa = value; }
        }

        private decimal _roe;
        public decimal Roe
        {
            get { return _roe; }
            set { _roe = value; }
        }

        private decimal? _dpa;
        public decimal? Dpa
        {
            get { return _dpa; }
            set { _dpa = (_dividendYield != null ? decimal.Round((decimal)_precoUnitario * ((decimal)_dividendYield / 100), 2) : null); }
        }

        private decimal? _payout;
        public decimal? Payout
        {
            get { return _payout; }
            set { _payout = (_lpa == 0 ? 0 : (_dpa != null ? decimal.Round(((decimal)_dpa / _lpa) * 100, 2) : null)); }
        }

        private decimal? _cagrLucro;
        public decimal? CAGRLucro
        {
            get { return _cagrLucro; }
            set { _cagrLucro = value; }
        }

        private decimal _crescimentoEsperado;
        public decimal CrescimentoEsperado
        {
            get { return _crescimentoEsperado; }
            set { _crescimentoEsperado = (_payout != null ? decimal.Round(((100 - (decimal)_payout) * _roe) / 100, 2) : _roe); }
        }

        private decimal _mediaCrescimento;
        public decimal MediaCrescimento
        {
            get { return _mediaCrescimento; }
            set { _mediaCrescimento = (_cagrLucro == null ? _crescimentoEsperado : decimal.Round(((decimal)_cagrLucro + _crescimentoEsperado) / 2, 2)); }
        }
    }
}
