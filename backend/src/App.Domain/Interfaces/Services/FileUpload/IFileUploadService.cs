using System.Collections.Generic;

namespace App.Domain.Interfaces.Services.FileUpload
{
    public interface IFileUploadService<T>
    {
        IEnumerable<T> GetLinesFile();
    }
}
