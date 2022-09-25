using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace App.Domain.Models.FilesImport
{
    public class FileFundamentus
    {
        private string _margemEbit;
        private string _dividendYeild;
        private string _roic;

        [Name("papel")]
        public string Ticker { get; set; }

        [Name("cotação", "cotacao", "cota,ìo")]
        public decimal Preco { get; set; }

        [Name("p/l")]
        public decimal? PrecoLucro { get; set; }

        [Name("div.yield")]
        public string DividendYeild
        {
            get { return _dividendYeild; }
            set { _dividendYeild = value.ToString().Replace("%", ""); }
        }

        [Name("mrg ebit")]
        public string MargemEbit
        {
            get { return _margemEbit; }
            set { _margemEbit = value.ToString().Replace("%", ""); }
        }

        [Name("ev/ebit")]
        public decimal? EvEbit { get; set; }

        [Name("roic")]
        public string Roic
        {
            get { return _roic; }
            set { _roic = value.ToString().Replace("%", ""); }
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
