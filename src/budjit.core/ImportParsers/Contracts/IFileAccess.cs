using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace budjit.core.ImportParsers.Contracts
{
    public interface IFileAccess
    {
        string ReadAllText(FileInfo fileInfo);
        bool Exists(FileInfo fileInfo);
    }
}
