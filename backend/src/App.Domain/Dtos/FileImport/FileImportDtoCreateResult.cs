using System;
using App.Domain.Enums;

namespace App.Domain.Dtos.FileImport
{
    public class FileImportDtoCreateResult
    {
        private TimeZoneInfo _curTimeZone = TimeZoneInfo.Local;
        private DateTime _dateCreated;
        private DateTime _dateFile;
        private TypeFileImport _typeFile;

        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated
        {
            get { return TimeZoneInfo.ConvertTimeFromUtc(_dateCreated, _curTimeZone); }
            set { _dateCreated = value; }
        }

        public DateTime DateFile
        {
            get { return TimeZoneInfo.ConvertTimeFromUtc(_dateFile, _curTimeZone); }
            set { _dateFile = value; }
        }

        public TypeFileImport TypeFile
        {
            get { return _typeFile; }
            set { _typeFile = value; }
        }

        public string NameTypeFile
        {
            get { return Enum.GetName(typeof(TypeFileImport), _typeFile); }
        }
    }
}
