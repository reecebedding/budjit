using budjit.core.data.Contracts;
using System;
using System.Collections.Generic;
using budjit.core.models;
using Microsoft.Data.Sqlite;

namespace budjit.core.data.SQLite
{
    public class TransactionRepository : ITransactionsRepository
    {
        private IBudjitContext context;
        public TransactionRepository(IBudjitContext context)
        {
            this.context = context;
        }

        Func<object, object> sanitizeNull = delegate (object value) { return value ?? DBNull.Value; };

        private SqliteCommand GetInsertCommand(Transaction transaction)
        {
            var insertCommand = new SqliteCommand();

            insertCommand.CommandText = "INSERT INTO transactions ( " +
                "date," +
                "merchant," +
                "description," +
                "alteration, " +
                "balance" +
                ") VALUES ( " +
                "$date, " +
                "$merchant, " +
                "$description, " +
                "$alteration, " +
                "$balance)";
            
            insertCommand.Parameters.AddWithValue("$date", sanitizeNull(transaction.Date));
            insertCommand.Parameters.AddWithValue("$merchant", sanitizeNull(transaction.Merchant));
            insertCommand.Parameters.AddWithValue("$description", sanitizeNull(transaction.Description));
            insertCommand.Parameters.AddWithValue("$alteration", sanitizeNull(transaction.Alteration));
            insertCommand.Parameters.AddWithValue("$balance", sanitizeNull(transaction.Balance));

            return insertCommand;
        }

        public void SaveTransaction(Transaction transaction)
        {
            var insertCommand = GetInsertCommand(transaction);
            context.Save(insertCommand);
        }

        public void SaveTransactions(IEnumerable<Transaction> transactions)
        {
            List<SqliteCommand> commands = new List<SqliteCommand>();
            foreach (Transaction trans in transactions)
            {
                commands.Add(GetInsertCommand(trans));
            }
            context.Save(commands);
        }
    }
}
