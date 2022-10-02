using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    public class BaseTickerEntity : BaseEntity
    {
        [Required]
        [MaxLength(10)]        
        public string BaseTicker { get; set; }

        [MaxLength(100)]
        public string Company { get; set; }

        [Required]
        public int SegmentId { get; set; }

        public SegmentEntity Segment { get; set; }
    }
}