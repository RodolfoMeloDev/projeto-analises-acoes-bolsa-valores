using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace App.Domain.Models.FilesImport
{
    public class FileFundamentus
    {
        private string _ticker;
        private string _margemEbit;
        private string _dividendYeild;
        private string _roic;

        [Name("papel")]
        public string Ticker
        {
            get { return _ticker; }
            set { _ticker = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        [Name("cotação", "cotacao", "cota,ìo")]
        public decimal Preco { get; set; }

        [Name("p/l")]
        public decimal? PrecoLucro { get; set; }

        [Name("div.yield")]
        public string DividendYeild
        {
            get { return _dividendYeild; }
            set { _dividendYeild = (string.IsNullOrEmpty(value) ? null : value.ToUpper().ToString().Replace("%", "")); }
        }

        [Name("mrg ebit")]
        public string MargemEbit
        {
            get { return _margemEbit; }
            set { _margemEbit = (string.IsNullOrEmpty(value) ? null : value.ToUpper().ToString().Replace("%", "")); }
        }

        [Name("ev/ebit")]
        public decimal? EvEbit { get; set; }

        [Name("roic")]
        public string Roic
        {
            get { return _roic; }
            set { _roic = (string.IsNullOrEmpty(value) ? null : value.ToUpper().ToString().Replace("%", "")); }
        }

        [Name("liq.2meses")]
        public decimal? LiquidezMediaDiaria { get; set; }

        [Name("p/vp")]
        public decimal? PrecoValorPatrimonial { get; set; }

        [Name("valor de mercado")]
        [Optional]
        public decimal? ValorMercado { get; set; }
    }
}
