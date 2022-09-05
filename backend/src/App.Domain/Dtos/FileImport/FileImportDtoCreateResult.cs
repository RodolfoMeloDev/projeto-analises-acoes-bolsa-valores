using System;

namespace App.Domain.Dtos.FileImport
{
    public class FileImportDtoCreateResult
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NomeArquivo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}