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
        public string Company { get; set; }

        [MaxLength(18)]
        public string CNPJ { get; set; }

        [Required]
        public TypeTicker TypeTicker { get; set; }

        public bool JudicialRecovery { get; set; }

        public int? SegmentId { get; set; }

        public SegmentEntity Segment { get; set; }
    }
}
