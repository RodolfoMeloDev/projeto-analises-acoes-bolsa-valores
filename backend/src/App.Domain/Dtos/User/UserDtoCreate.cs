using System.ComponentModel.DataAnnotations;

namespace App.Domain.Dtos.User
{
    public class UserDtoCreate
    {

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(20, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Password { get; set; }

        [StringLength(30, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string NickName { get; set; }
    }
}
