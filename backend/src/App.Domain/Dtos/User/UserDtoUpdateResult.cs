using System;

namespace App.Domain.Dtos.User
{
    public class UserDtoUpdateResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataAlteracao { get; set; }        
    }
}