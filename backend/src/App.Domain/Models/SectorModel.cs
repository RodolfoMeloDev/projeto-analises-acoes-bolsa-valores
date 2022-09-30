namespace App.Domain.Models
{
    public class SectorModel : BaseModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

    }
}
