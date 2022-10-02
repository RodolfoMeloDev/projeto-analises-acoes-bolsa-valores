namespace App.Domain.Models
{
    public class BaseTickerModel
    {
        private string _baseTicker;
        public string BaseTicker
        {
            get { return _baseTicker; }
            set { _baseTicker = value; }
        }
        
        private string _company;
        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }

        private int _segmentId;
        public int SegmentId
        {
            get { return _segmentId; }
            set { _segmentId = value; }
        }        
    }
}