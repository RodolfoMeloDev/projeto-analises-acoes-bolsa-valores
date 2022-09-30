using System.ComponentModel.DataAnnotations;

namespace App.Domain.Dtos.Sector
{
    public class SectorDtoUpdate
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Name { get; set; }   

        public bool Active { get; set; }
    }
}