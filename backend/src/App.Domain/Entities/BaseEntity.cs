using System;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public abstract class BaseEntity
    {
        const string STATUS_ATIVO = "A";
        private string _status;
        private DateTime? _dataCadastro;

        [Key]
        public int Id { get; set; } 

        [Required]
        [MaxLength(1)]
        public string Status { 
            get { return _status; }
            set { _status = value == null ? STATUS_ATIVO : value; } 
        }

        [Required]
        public DateTime? DataCadastro { 
            get { return _dataCadastro; } 
            set { _dataCadastro = (value == null? DateTime.UtcNow : value); } }

        public DateTime? DataAlteracao { get; set; }
    }
}