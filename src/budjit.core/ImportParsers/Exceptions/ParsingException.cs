using System;
using System.Collections.Generic;
using System.Text;

namespace budjit.core.ImportParsers.Exceptions
{
    public class ParsingException : Exception
    {
        public ParsingException() { }
        
        public ParsingException(string message) : base(message) { }
        public ParsingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
