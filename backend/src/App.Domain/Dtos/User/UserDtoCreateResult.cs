using System;

namespace App.Domain.Dtos.User
{
    public class UserDtoCreateResult : BaseCreateDtoResult
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }        
    }
}