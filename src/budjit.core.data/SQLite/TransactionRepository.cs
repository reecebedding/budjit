using budjit.core.data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using budjit.core.models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace budjit.core.data.SQLite
{
    public class TransactionRepository : ITransactionsRepository
    {
        private BudjitContext db;
        public TransactionRepository(BudjitContext context)
        {
            this.db = context;
        }

        public IEnumerable<Transaction> GetAll()
        {
            return db.Transactions;
        }

        public Transaction GetTransactionById(int id)
        {
            return db.Transactions.Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Transaction> GetTransactionInDateRange(DateTime start, DateTime end)
        {
            return db.Transactions.Where(x => (x.Date >= start && x.Date <= end));
        }

        public void SaveTransaction(Transaction transaction)
        {
            db.Transactions.Add(transaction);
            db.SaveChanges();
        }

        public void SaveTransactions(IEnumerable<Transaction> transactions)
        {
            db.Transactions.AddRange(transactions);
            db.SaveChanges();
        }
    }
}
