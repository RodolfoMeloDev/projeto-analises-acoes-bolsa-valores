namespace App.Domain.Models
{
    public class UserModel : BaseModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private string _nickName;
        public string NickName
        {
            get { return _nickName; }
            set { _nickName = value; }
        }

    }
}
