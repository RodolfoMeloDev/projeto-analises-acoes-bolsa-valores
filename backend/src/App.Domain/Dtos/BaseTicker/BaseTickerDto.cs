using App.Domain.Dtos.Segment;

namespace App.Domain.Dtos.BaseTicker
{
    public class BaseTickerDto : BaseDto
    {
        public string BaseTicker { get; set; }
        public string Company { get; set; }
        public int SegmentId { get; set; }
    }
}