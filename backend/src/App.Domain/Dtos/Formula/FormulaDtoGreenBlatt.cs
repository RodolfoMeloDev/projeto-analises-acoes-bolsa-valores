namespace App.Domain.Dtos.Formula
{
    public class FormulaDtoGreenBlatt : FormulaDto
    {
        public int Position { get; set; }
        public int EvEbitScore { get; set; }
        public int RoicScore { get; set; }
        public int FinalScore { get; set; }
    }
}
