namespace App.Domain.Models
{
    public class SegmentModel : BaseModel
    {
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private int _subSetorId;
        public int SubSetorId
        {
            get { return _subSetorId; }
            set { _subSetorId = value; }
        }

    }
}
