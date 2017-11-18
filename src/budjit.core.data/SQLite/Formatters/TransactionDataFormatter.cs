using budjit.core.data.SQLite.Formatter;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using budjit.core.models;

namespace budjit.core.data.SQLite.Formatters
{
    public class TransactionDataFormatter : IDataFormatter
    {
        public object Format(SqliteDataReader reader)
        {
            return new Transaction()
            {
                Date = reader.GetDateTime(1),
                Merchant = reader.GetString(2),
                Description = reader.GetString(3),
                Alteration = reader.GetDecimal(4),
                Balance = reader.GetDecimal(5)
            };
        }
    }
}
