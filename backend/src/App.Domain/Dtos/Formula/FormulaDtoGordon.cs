namespace App.Domain.Dtos.Formula
{
    public class FormulaDtoGordon : FormulaDto
    {
        public decimal JustPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal MarketRisk { get; set; }
    }
}
