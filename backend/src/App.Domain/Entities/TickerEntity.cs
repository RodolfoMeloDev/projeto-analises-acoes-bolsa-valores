using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;

namespace App.Domain.Entities
{
    public class TickerEntity : BaseEntity
    {
        [Required]
        public string BaseTicker { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string Ticker { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(100)]
        public string Empresa { get; set; }

        [MaxLength(18)]
        public string CNPJ { get; set; }

        [MaxLength(2000)]
        public string Descricao { get; set; }

        [MaxLength(200)]
        public string Site { get; set; }

        [Required]
        public TypeTicker Tipo { get; set; }

        public bool RecuperacaoJudicial { get; set; }

        [Required]
        public int SegmentoId { get; set; }

        public SegmentEntity Segmento { get; set; }
    }
}