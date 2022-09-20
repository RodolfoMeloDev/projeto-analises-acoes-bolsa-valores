namespace App.Domain.Dtos.HistoryTicker
{
    public class HistoryTickerDtoCreateResult : BaseCreateDtoResult
    {
        public int ArquivoImportacaoId { get; set; }    
        public int TickerId { get; set; }
        public float PrecoUnitario { get; set; }
        public float PrecoLucro { get; set; }
        public float Roic { get; set; }
        public float EvEbit { get; set; }
        public float MargemEbit { get; set; }
        public float DividendYield { get; set; }
        public float PrecoValorPatrimonial { get; set; }
        public float LiquidezMediaDiaria { get; set; }
        public float ValorMercado { get; set; }
        public float VolumeFinanceiro { get; set; }
    }
}