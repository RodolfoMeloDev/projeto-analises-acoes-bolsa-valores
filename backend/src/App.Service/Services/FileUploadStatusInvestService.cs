using System.Globalization;
using System.Text;
using App.Domain.Interfaces.Services.FileUpload;
using App.Domain.Models.FilesImport;
using App.Service.Services.Exceptions;
using App.Service.Services.Functions;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;

namespace App.Service.Services
{
    public class FileUploadStatusInvestService : IFileUploadService<FileStatusInvest>
    {
        private IFormFile _file;
        private string _directoryUser;
        private string? _pathFile;

        public FileUploadStatusInvestService(IFormFile file, string directoryUser)
        {
            _file = file;
            _directoryUser = directoryUser;
            _pathFile = Utils.CopyFileToServer(_file, _directoryUser);
        }

        public IEnumerable<FileStatusInvest> GetLinesFile()
        {
            try
            {
                if (!String.IsNullOrEmpty(_pathFile))
                {
                    var config = new CsvConfiguration(CultureInfo.CurrentCulture)
                    {
                        HasHeaderRecord = true,
                        TrimOptions = TrimOptions.Trim,
                        PrepareHeaderForMatch = args => args.Header.ToLower(),
                    };

                    using (var reader = new StreamReader(_pathFile, Encoding.Latin1))
                    using (var csv = new CsvReader(reader, config))
                    {
                        return csv.GetRecords<FileStatusInvest>().ToList();
                    }
                }
                else
                    throw new FileUploadStatusInvestException("Não foi possível definir um local para descarregar o arquivo no servidor.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
