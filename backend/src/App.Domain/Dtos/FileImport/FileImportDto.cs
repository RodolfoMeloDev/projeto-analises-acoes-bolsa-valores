using System;
using App.Domain.Dtos.User;

namespace App.Domain.Dtos.FileImport
{
    public class FileImportDto : BaseDto
    {
        public int UsuarioId { get; set; }
        public UserDto Usuario { get; set; }
        public string NomeArquivo { get; set; }
    }
}