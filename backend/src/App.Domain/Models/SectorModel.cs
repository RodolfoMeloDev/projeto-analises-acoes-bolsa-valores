namespace App.Domain.Models
{
    public class SectorModel : BaseModel
    {
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        
    }
}