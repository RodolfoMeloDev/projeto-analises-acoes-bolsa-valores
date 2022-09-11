using System;
using App.Domain.Enums;

namespace App.Domain.Dtos.Ticker
{
    public class TickerDtoCreateResult : BaseCreateDtoResult
    {
        public string BaseTicker { get; set; }
        public string Ticker { get; set; }
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public string CNPJ { get; set; }
        public string Descricao { get; set; }
        public string Site { get; set; }
        public TypeTicker Tipo { get; set; }
        public bool RecuperacaoJudicial { get; set; }
        public int SegmentoId { get; set; }            
    }
}