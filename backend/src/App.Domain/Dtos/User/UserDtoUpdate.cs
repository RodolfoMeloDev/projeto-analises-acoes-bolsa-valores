using System.ComponentModel.DataAnnotations;

namespace App.Domain.Dtos.User
{
    public class UserDtoUpdate
    {       
        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(20, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }        

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(20, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Senha { get; set; }

        public bool Ativo { get; set; }
    }
}