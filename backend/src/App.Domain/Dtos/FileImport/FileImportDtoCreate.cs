using System;
using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace App.Domain.Dtos.FileImport
{
    public class FileImportDtoCreate
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo deve ter no máximo {1} caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public DateTime DataArquivo { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public TypeFileImport TipoArquivo { get; set; }

        public IFormFile File { get; set; }
    }
}
