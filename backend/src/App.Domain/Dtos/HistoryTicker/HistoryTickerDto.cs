using App.Domain.Dtos.FileImport;
using App.Domain.Dtos.Ticker;

namespace App.Domain.Dtos.HistoryTicker
{
    public class HistoryTickerDto : BaseDto
    {
        public HistoryTickerDto(int historicoTickerId, int tickerId, float precoUnitario, float roic, float margemEbit, float precoValorPatrimonial, float valorMercado) 
        {
            this.HistoricoTickerId = historicoTickerId;
    this.TickerId = tickerId;
    this.PrecoUnitario = precoUnitario;
    this.Roic = roic;
    this.MargemEbit = margemEbit;
    this.PrecoValorPatrimonial = precoValorPatrimonial;
    this.ValorMercado = valorMercado;
   
        }
                public int HistoricoTickerId { get; set; }
        public FileImportDto ArquivoImportacao { get; set; }
        public int TickerId { get; set; }
        public TickerDto Ticker { get; set; }
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