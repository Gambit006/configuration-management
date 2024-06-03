using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagement.Extceptions
{
    public class UndefinedDataTypeException : Exception
    {
        public UndefinedDataTypeException() : base() { }
        public UndefinedDataTypeException(string message) : base(message) { }
        public UndefinedDataTypeException(string message, Exception inner) : base(message, inner) { }
    }
}
