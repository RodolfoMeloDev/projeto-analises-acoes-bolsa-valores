using System;
using App.Domain.Dtos.User;

namespace App.Domain.Dtos.FileImport
{
    public class FileImportDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public UserDto Usuario { get; set; }
        public string NomeArquivo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}