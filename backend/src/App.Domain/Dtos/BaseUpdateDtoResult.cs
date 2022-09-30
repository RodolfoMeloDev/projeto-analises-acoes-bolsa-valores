using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Dtos
{
    public class BaseUpdateDtoResult
    {
        private TimeZoneInfo _curTimeZone = TimeZoneInfo.Local;
        private DateTime _dateUpdated;

        public int Id { get; set; }        
        public bool Active { get; set; }
        
        public DateTime? DateUpdated 
        {
            get { return TimeZoneInfo.ConvertTimeFromUtc(_dateUpdated, _curTimeZone); }
            set { _dateUpdated = (DateTime)value; }
        }
    }
}