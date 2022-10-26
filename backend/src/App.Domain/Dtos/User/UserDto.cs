using System;

namespace App.Domain.Dtos.User
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
