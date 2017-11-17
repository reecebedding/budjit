using System;
using System.Collections.Generic;
using System.Text;

namespace budjit.core.ImportParsers.Exceptions
{
    public class InvalidCSVException : Exception
    {
        public InvalidCSVException(string message):base(message)
        {

        }
    }
}
