using System;

namespace App.Domain.Dtos.User
{
    public class UserDtoUpdateResult : BaseUpdateDtoResult
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
