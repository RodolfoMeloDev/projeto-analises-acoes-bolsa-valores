using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Dtos
{
    public class BaseDto
    {
        private TimeZoneInfo _curTimeZone = TimeZoneInfo.Local;
        private DateTime _dateCreated;
        private DateTime? _dateUpdated;

        public int Id { get; set; }
        public bool Active { get; set; }

        public DateTime DateCreated 
        { 
            get { return TimeZoneInfo.ConvertTimeFromUtc(_dateCreated, _curTimeZone); }
            set { _dateCreated = value ; } 
        }

        public DateTime? DateUpdated 
        {
            get { return _dateUpdated != null ? TimeZoneInfo.ConvertTimeFromUtc(_dateCreated, _curTimeZone) : null ; }
            set { _dateUpdated = value; }
        }
    }
}