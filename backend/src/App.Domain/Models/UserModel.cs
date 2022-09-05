using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public class UserModel : BaseModel
    {
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        
        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set { _senha = value; }
        }
        
    }
}