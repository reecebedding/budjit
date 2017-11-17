using budjit.core.ImportParsers.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace budjit.core.ImportParsers
{
    public class FileAccess : IFileAccess
    {
        public bool Exists(FileInfo fileInfo)
        {
            return File.Exists(fileInfo.ToString());
        }

        public string ReadAllText(FileInfo fileInfo)
        {
            return File.ReadAllText(fileInfo.ToString());
        }
    }
}
