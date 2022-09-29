using CsvHelper.Configuration.Attributes;

namespace App.Domain.Models.FilesImport
{
    public class FileStatusInvest
    {
        private string _ticker;

        [Name("ticker")]
        public string Ticker
        {
            get { return _ticker; }
            set { _ticker = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        [Name("preco")]
        public decimal Preco { get; set; }

        [Name("p/l")]
        public decimal? PrecoLucro { get; set; }

        [Name("dy")]
        public decimal? DividendYeild { get; set; }

        [Name("margem ebit")]
        public decimal? MargemEbit { get; set; }

        [Name("ev/ebit")]
        public decimal? EvEbit { get; set; }

        [Name("roic")]
        public decimal? Roic { get; set; }

        [Name("liquidez media diaria")]
        public decimal? LiquidezMediaDiaria { get; set; }

        [Name("p/vp")]
        public decimal? PrecoValorPatrimonial { get; set; }

        [Name("valor de mercado")]
        public decimal? ValorMercado { get; set; }

        [Name("lpa")]
        public decimal? Lpa { get; set; }

        [Name("roe")]
        public decimal? Roe { get; set; }

        [Name("cagr lucros 5 anos")]
        public decimal? CAGRLucro { get; set; }

        [Name("vpa")]
        public decimal? Vpa { get; set; }
    }
}
