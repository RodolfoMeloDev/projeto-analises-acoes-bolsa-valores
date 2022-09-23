using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;

namespace App.Domain.Entities
{
    public class TickerEntity : BaseEntity
    {
        [MaxLength(10)]
        public string BaseTicker { get; set; }

        [Required]
        [MaxLength(10)]
        public string Ticker { get; set; }

        [MaxLength(100)]
        public string Empresa { get; set; }

        [MaxLength(18)]
        public string CNPJ { get; set; }

        [Required]
        public TypeTicker Tipo { get; set; }

        public bool RecuperacaoJudicial { get; set; }

        public int? SegmentoId { get; set; }

        public SegmentEntity Segmento { get; set; }
    }
}
