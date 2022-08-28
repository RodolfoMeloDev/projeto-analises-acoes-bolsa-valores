using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public class SegmentEntity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        public int SubSetorId { get; set; }

        public SubSectorEntity SubSetor { get; set; }
    }
}