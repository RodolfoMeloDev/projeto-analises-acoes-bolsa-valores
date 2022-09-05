using System;

namespace App.Domain.Dtos.User
{
    public class UserDtoCreateResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}