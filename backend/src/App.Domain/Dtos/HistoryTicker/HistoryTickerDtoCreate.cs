using System.ComponentModel.DataAnnotations;

namespace App.Domain.Dtos.HistoryTicker
{
    public class HistoryTickerDtoCreate
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        public int FileImportId { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public int TickerId { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal PriceByProfit { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal Roic { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal EvEbit { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal EbitMargin { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal Lpa { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal Vpa { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal Roe { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public decimal ExpectedGrowth { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
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
