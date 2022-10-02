using App.Domain.Dtos.Segment;

namespace App.Domain.Dtos.BaseTicker
{
    public class BaseTickerDtoComplete : BaseDto
    {
        public string BaseTicker { get; set; }
        public string Company { get; set; }
        public int SegmentId { get; set; }
        public SegmentDtoComplete Segment { get; set; }  
    }
}