using System.ComponentModel.DataAnnotations;

namespace App.Domain.Dtos.Sector
{
    public class SectorDtoCreate
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }
    }
}