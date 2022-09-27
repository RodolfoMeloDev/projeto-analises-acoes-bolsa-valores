using System;
using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;

namespace App.Domain.Entities
{
    public class FileImportEntity : BaseEntity
    {
        private DateTime _dataArquivo;
        [Required]
        public int UsuarioId { get; set; }

        public UserEntity Usuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string NomeArquivo { get; set; }

        [Required]
        [MaxLength(100)]
        public string Descricao { get; set; }

        [Required]
        public DateTime DataArquivo
        {
            get { return _dataArquivo; }
            set { _dataArquivo = (value == DateTime.MinValue ? DateTime.UtcNow : value); }
        }

        [Required]
        public TypeFileImport TipoArquivo { get; set; }
    }
}
