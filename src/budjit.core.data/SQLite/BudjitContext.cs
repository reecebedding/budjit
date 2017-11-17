using Microsoft.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace budjit.core.data.SQLite
{
    public class BudjitContext : IBudjitContext
    {
        private static string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private SqliteConnection SqliteConnection => new SqliteConnection($"DataSource={path}\\SQLite\\budjit.db");

        public void Save(SqliteCommand command)
        {
            using (var connection = SqliteConnection)
            {
                connection.Open();
                using (var sqlTransaction = connection.BeginTransaction())
                {
                    connection.Open();

                    command.Transaction = sqlTransaction;

                    command.ExecuteNonQuery();

                    sqlTransaction.Commit();
                }
            }
        }

        public void Save(IEnumerable<SqliteCommand> commands)
        {
            using (var connection = SqliteConnection)
            {
                connection.Open();
                using (var sqlTransaction = connection.BeginTransaction())
                {
                    foreach (SqliteCommand command in commands)
                    {
                        command.Connection = connection;
                        command.Transaction = sqlTransaction;

                        command.ExecuteNonQuery();
                    }

                    sqlTransaction.Commit();
                }
            }
        }
    }
}
