using System;

namespace App.Domain.Dtos.User
{
    public class UserDtoUpdateResult : BaseUpdateDtoResult
    {        
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }        
    }
}