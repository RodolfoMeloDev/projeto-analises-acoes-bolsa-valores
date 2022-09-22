using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Service.Services.Exceptions
{
    public class FileUploadFundamentusException : ApplicationException
    {
        public FileUploadFundamentusException(string message) : base(message)
        {
        }
    }
}
