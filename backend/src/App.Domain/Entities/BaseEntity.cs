using System;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public abstract class BaseEntity
    {
        private DateTime? _dataCadastro;

        [Key]
        public int Id { get; set; } 

        [Display(Name ="Status")]
        public bool Ativo { get; set; }

        [Required]
        public DateTime? DataCadastro { 
            get { return _dataCadastro; } 
            set { _dataCadastro = (value == null? DateTime.UtcNow : value); } }

        public DateTime? DataAlteracao { get; set; }
    }
}