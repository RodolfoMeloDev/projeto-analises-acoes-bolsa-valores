using System.ComponentModel.DataAnnotations;

namespace App.Domain.Dtos.Segment
{
    public class SegmentDtoCreate
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public int SubSectorId { get; set; }
    }
}