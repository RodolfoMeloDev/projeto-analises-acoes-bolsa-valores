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
        public string Nome { get; set; }

        [Required]
        public int SetorId { get; set; }

        public SectorEntity Setor { get; set; }

        public IEnumerable<SegmentEntity> Segmentos { get; set; }
    }
}