using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public class SegmentEntity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int SubSectorId { get; set; }

        public SubSectorEntity SubSector { get; set; }
    }
}