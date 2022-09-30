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
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }        
    }
}