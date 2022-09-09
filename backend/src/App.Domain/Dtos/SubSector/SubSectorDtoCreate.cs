using System.ComponentModel.DataAnnotations;

namespace App.Domain.Dtos.SubSector
{
    public class SubSectorDtoCreate
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public int SetorId { get; set; }
    }
}