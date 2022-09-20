using System;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Dtos.FileImport
{
    public class FileImportDtoCreate
    {
        [Required(ErrorMessage = "O campo é Obrigatório")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string NomeArquivo { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public DateTime DataArquivo { get; set; }
    }
}