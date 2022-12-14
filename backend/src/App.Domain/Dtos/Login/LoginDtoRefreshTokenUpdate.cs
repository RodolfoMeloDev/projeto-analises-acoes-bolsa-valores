using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Dtos.Login
{
    public class LoginDtoRefreshTokenUpdate
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public string RefreshToken { get; set; }
    }
}
