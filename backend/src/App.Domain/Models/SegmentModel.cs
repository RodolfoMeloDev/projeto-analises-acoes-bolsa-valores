namespace App.Domain.Models
{
    public class SegmentModel : BaseModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private int _subSectorId;
        public int SubSectorId
        {
            get { return _subSectorId; }
            set { _subSectorId = value; }
        }

    }
}
