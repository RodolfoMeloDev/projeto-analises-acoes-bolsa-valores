using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;

namespace App.Domain.Entities
{
    public class TickerEntity : BaseEntity
    {
        [Required]
        [MaxLength(10)]
        public string Ticker { get; set; }

        [Required]
        public int BaseTickerId { get; set; }

        public BaseTickerEntity BaseTicker { get; set; }    

        [Required]
        public TypeTicker TypeTicker { get; set; }

        public bool JudicialRecovery { get; set; }
    }
}
