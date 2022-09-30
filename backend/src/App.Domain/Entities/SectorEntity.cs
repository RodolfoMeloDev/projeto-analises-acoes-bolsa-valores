using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public class SectorEntity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public IEnumerable<SubSectorEntity> SubSectors { get; set; }
    }
}