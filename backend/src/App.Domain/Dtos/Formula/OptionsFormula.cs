namespace App.Domain.Dtos.Formula
{
    public class OptionsFormula
    {
        public int FileImportId { get; set; }
        public bool IncluirItensRecuperacaoJudicial { get; set; }
        public bool RemoverMenorLiquidez { get; set; }
        public bool RemoverItensComValorZerado { get; set; }
        public bool RemoverItensComValorNegativo { get; set; }
        public decimal? LiquidezMinima { get; set; }
        public decimal? EvEbitMinima { get; set; }
        public decimal? EvEbitMaxima { get; set; }
        public decimal? PLMinima { get; set; }
        public decimal? PLMaxima { get; set; }
        public decimal? MargemEbitMinima { get; set; }
        public decimal? MargemEbitMaxima { get; set; }
        public decimal? RiscoBolsa { get; set; }

    }
}
