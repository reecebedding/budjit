using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace budjit.core.data.SQLite.Formatter
{
    public interface IDataFormatter
    {
        object Format(SqliteDataReader reader);
    }
}
