using System.ComponentModel.DataAnnotations;

namespace App.Domain.Dtos.SubSector
{
    public class SubSectorDtoUpdate
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public int SetorId { get; set; }
        
        public bool Ativo { get; set; }
    }
}