using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Dtos
{
    public class BaseUpdateDtoResult
    {
        private TimeZoneInfo _curTimeZone = TimeZoneInfo.Local;
        private DateTime _dataAlteracao;

        public int Id { get; set; }        
        public bool Ativo { get; set; }
        
        public DateTime? DataAlteracao 
        {
            get { return TimeZoneInfo.ConvertTimeFromUtc(_dataAlteracao, _curTimeZone); }
            set { _dataAlteracao = (DateTime)value; }
        }
    }
}