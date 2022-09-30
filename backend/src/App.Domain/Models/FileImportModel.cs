using System;
using App.Domain.Enums;

namespace App.Domain.Models
{
    public class FileImportModel : BaseModel
    {
        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = (string.IsNullOrEmpty(value) ? null : value.ToUpper()); }
        }

        private DateTime _dateFile;
        public DateTime DateFile
        {
            get { return _dateFile; }
            set { _dateFile = value; }
        }

        private TypeFileImport _typeFile;
        public TypeFileImport TypeFile
        {
            get { return _typeFile; }
            set { _typeFile = value; }
        }


    }
}
