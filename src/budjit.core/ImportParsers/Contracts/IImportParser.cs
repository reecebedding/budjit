using budjit.core.Models;
using System.Collections.Generic;

namespace budjit.core.ImportParsers.Contracts
{
    public interface IImportParser
    {
        IEnumerable<Transaction> Parse();
    }
}
