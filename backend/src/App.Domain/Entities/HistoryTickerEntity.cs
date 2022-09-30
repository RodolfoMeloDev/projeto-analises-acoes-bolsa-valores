using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public class HistoryTickerEntity : BaseEntity
    {
        [Required]
        public int FileImportId { get; set; }

        public FileImportEntity FileImport { get; set; }

        [Required]
        public int TickerId { get; set; }

        public TickerEntity Ticker { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal PrecoUnitario { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal Roic { get; set; }

        [Required]
        public decimal EvEbit { get; set; }

        [Required]
        public decimal EbitMargin { get; set; }

        [Required]
        public decimal Lpa { get; set; }

        [Required]
        public decimal Vpa { get; set; }

        [Required]
        public decimal Roe { get; set; }

        [Required]
        public decimal ExpectedGrowth { get; set; }

        [Required]
        public decimal AverageGrowth { get; set; }

        public decimal? DividendYield { get; set; }
        public decimal? Pvp { get; set; }
        public decimal? Dpa { get; set; }
        public decimal? Payout { get; set; }
        public decimal? ProfitCAGR { get; set; }
        public decimal? AverageDailyLiquidity { get; set; }
        public decimal? MarketValue { get; set; }
    }
}
