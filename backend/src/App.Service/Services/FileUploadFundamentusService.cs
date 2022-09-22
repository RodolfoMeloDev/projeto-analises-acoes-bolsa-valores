using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Interfaces.Services.FileUpload;
using App.Domain.Models.FilesImport;
using App.Service.Services.Exceptions;
using App.Service.Services.Functions;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;

namespace App.Service.Services
{
    public class FileUploadFundamentusService : IFileUploadService<FileFundamentus>
    {
        private IFormFile _file;
        private string _directoryUser;
        private string? _pathFile;

        public FileUploadFundamentusService(IFormFile file, string directoryUser)
        {
            _file = file;
            _directoryUser = directoryUser;
            _pathFile = Utils.CopyFileToServer(_file, _directoryUser);
        }

        public IEnumerable<FileFundamentus> GetLinesFile()
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
                        return csv.GetRecords<FileFundamentus>().ToList();
                    }

                    // string tempName = Path.GetTempFileName();

                    // using (StreamReader sr = new StreamReader(_pathFile, Encoding.Latin1, false))
                    // {
                    //     using (StreamWriter sw = new StreamWriter(tempName, false, Encoding.UTF8))
                    //     {
                    //         int charsRead;
                    //         char[] buffer = new char[128 * 1024];
                    //         while ((charsRead = sr.ReadBlock(buffer, 0, buffer.Length)) > 0)
                    //         {
                    //             sw.Write(buffer, 0, charsRead);
                    //         }
                    //     }
                    // }
                    // File.Delete(_pathFile);
                    // File.Move(tempName, _pathFile);

                    // using (var reader = new StreamReader(_pathFile))
                    // using (var csv = new CsvReader(reader, config))
                    // {
                    //     return csv.GetRecords<FileFundamentus>().ToList();
                    // }
                }
                else
                    throw new FileUploadFundamentusException("Não foi possível definir um local para descarregar o arquivo no servidor.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
