using System;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public abstract class BaseEntity
    {
        private DateTime? _dateCreated;
        private DateTime? _dateUpdated;

        [Key]
        public int Id { get; set; }

        public bool Active { get; set; }

        [Required]
        public DateTime? DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = (value == null ? DateTime.UtcNow : value); }
        }

        public DateTime? DateUpdated
        {
            get { return _dateUpdated; }
            set { _dateUpdated = (value == DateTime.MinValue ? null : value); }
        }
    }
}
