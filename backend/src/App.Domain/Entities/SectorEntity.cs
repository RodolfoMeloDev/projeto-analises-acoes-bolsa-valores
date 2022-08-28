using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public class SectorEntity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        public IEnumerable<SubSectorEntity> SubSetores { get; set; }
    }
}