using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Service.Services.Exceptions
{
    public class FormulaException : ApplicationException
    {
        public FormulaException(string? message) : base(message)
        {
        }
    }
}
