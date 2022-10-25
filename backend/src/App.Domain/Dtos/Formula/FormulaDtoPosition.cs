using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Dtos.Formula
{
    public class FormulaDtoPosition : FormulaDto
    {
        public int? PositionGreenBlatt { get; set; }
        public int? PositionEvEbit { get; set; }
        public int? PositionPriceAndProfit { get; set; }
        public decimal? JustPriceBazin { get; set; }
        public decimal? DiscountPercentageBazin { get; set; }
        public decimal? JustPriceGraham { get; set; }
        public decimal? DiscountPercentageGraham { get; set; }
        public decimal? JustPriceGordon { get; set; }
        public decimal? DiscountPercentageGordon { get; set; }
        public decimal MarketRisk { get; set; }
    }
}
