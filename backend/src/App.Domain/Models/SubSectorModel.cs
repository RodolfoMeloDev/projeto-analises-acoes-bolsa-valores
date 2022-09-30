namespace App.Domain.Models
{
    public class SubSectorModel : BaseModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private int _sectorId;
        public int SectorId
        {
            get { return _sectorId; }
            set { _sectorId = value; }
        }
    }
}
