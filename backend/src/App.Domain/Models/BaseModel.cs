using System;

namespace App.Domain.Models
{
    public class BaseModel
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private bool _active;
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
        
        private DateTime _dateCreated;
        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value == null ? DateTime.UtcNow : value; }
        }

        private DateTime _dateUpdated;
        public DateTime DateUpdated
        {
            get { return _dateUpdated; }
            set { _dateUpdated = value; }
        }                
    }
}