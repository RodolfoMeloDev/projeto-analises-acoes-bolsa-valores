using System;
using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;

namespace App.Domain.Entities
{
    public class FileImportEntity : BaseEntity
    {
        private DateTime _dateFile;
        [Required]
        public int UserId { get; set; }

        public UserEntity User { get; set; }

        [Required]
        [MaxLength(100)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public DateTime DateFile
        {
            get { return _dateFile; }
            set { _dateFile = (value == DateTime.MinValue ? DateTime.UtcNow : value); }
        }

        [Required]
        public TypeFileImport TypeFile { get; set; }
    }
}
