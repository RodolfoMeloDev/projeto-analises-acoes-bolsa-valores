using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public class FileImportEntity : BaseEntity
    {
        [Required]
        public int UsuarioId { get; set; }

        public UserEntity Usuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string NomeArquivo { get; set; }        
    }
}