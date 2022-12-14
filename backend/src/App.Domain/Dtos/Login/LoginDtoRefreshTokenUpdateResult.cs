using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Dtos.Login
{
    public class LoginDtoRefreshTokenUpdateResult : BaseUpdateDtoResult
    {
        public string Login { get; set; }
        public string RefreshToken { get; set; }
    }
}
