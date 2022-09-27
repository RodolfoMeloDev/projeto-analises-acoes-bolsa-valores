namespace App.Domain.Models
{
    public class SubSectorModel : BaseModel
    {
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private int _setorId;
        public int SetorId
        {
            get { return _setorId; }
            set { _setorId = value; }
        }
    }
}
