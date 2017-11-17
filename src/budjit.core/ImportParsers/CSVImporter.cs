using budjit.core.ImportParsers.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace budjit.core.ImportParsers
{
    public class CSVImporter : IImporter
    {
        private FileInfo fileInfo;
        private IFileAccess fileAccess;

        public CSVImporter(FileInfo fileInfo) : this(fileInfo, new FileAccess()) { }
        public CSVImporter(FileInfo fileInfo, IFileAccess fileAccess)
        {
            this.fileInfo = fileInfo;
            this.fileAccess = fileAccess;
        }
        public string Import()
        {
            if (!fileAccess.Exists(fileInfo))
                throw new FileNotFoundException();

            string data = fileAccess.ReadAllText(fileInfo);
            return data;
        }
    }
}
