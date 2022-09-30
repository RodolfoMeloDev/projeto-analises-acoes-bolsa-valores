using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    public class SubSectorEntity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int SectorId { get; set; }

        public SectorEntity Sector { get; set; }

        public IEnumerable<SegmentEntity> Segments { get; set; }
    }
}