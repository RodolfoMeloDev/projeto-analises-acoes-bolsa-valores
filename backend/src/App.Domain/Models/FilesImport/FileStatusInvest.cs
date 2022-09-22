using CsvHelper.Configuration.Attributes;

namespace App.Domain.Models.FilesImport
{
    public class FileStatusInvest
    {
        [Name("ticker")]
        public string Ticker { get; set; }

        [Name("preco")]
        public float Preco { get; set; }

        [Name("p/l")]
        public float? PrecoLucro { get; set; }

        [Name("dy")]
        public float? DividendYeild { get; set; }

        [Name("margem ebit")]
        public float? MargemEbit { get; set; }

        [Name("ev/ebit")]
        public float? EvEbit { get; set; }

        [Name("roic")]
        public float? Roic { get; set; }

        [Name("liquidez media diaria")]
        public double? LiquidezMediaDiaria { get; set; }

        [Name("p/vp")]
        public double? PrecoValorPatrimonial { get; set; }

        [Name("valor de mercado")]
        public double? ValorMercado { get; set; }
    }
}
