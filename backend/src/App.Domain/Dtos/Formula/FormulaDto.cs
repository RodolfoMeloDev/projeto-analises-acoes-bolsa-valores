namespace App.Domain.Dtos.Formula
{
    public class FormulaDto
    {
        public string BaseTicker { get; set; }
        public string Ticker { get; set; }
        public decimal Preco { get; set; }
        public decimal DividendYield { get; set; }
        public decimal PrecoLucro { get; set; }
        public decimal EvEbit { get; set; }
        public decimal Roic { get; set; }
        public decimal MargemEbit { get; set; }
        public decimal LiquidezMediaDiaria { get; set; }
        public bool RecuperacaoJudicial { get; set; }
    }
}