using budjit.core.data.SQLite.Formatter;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace budjit.core.data.SQLite
{
    public interface IBudjitContext
    {
        void Save(SqliteCommand command);
        void Save(IEnumerable<SqliteCommand> commands);
        IEnumerable<object> Query(SqliteCommand command, IDataFormatter formatter);
    }
}
