using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string Login { get; set; }

        [Required]
        [MaxLength(20)]
        public string Senha { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }        
    }
}