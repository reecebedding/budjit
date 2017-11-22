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
            return db.Transactions
                .Include(trans => trans.Tag);
        }

        public Transaction GetTransactionById(int id)
        {
            return db.Transactions
                .Include(trans => trans.Tag)
                .Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Transaction> GetTransactionInDateRange(DateTime start, DateTime end)
        {
            return db.Transactions
                .Include(trans => trans.Tag)
                .Where(x => (x.Date >= start && x.Date <= end));
        }

        public void SaveTransaction(Transaction transaction)
        {
            if (transaction.ID > 0)
                db.Transactions.Update(transaction);
            else
                db.Transactions.Add(transaction);
            db.SaveChanges();
        }

        public void SaveTransactions(IEnumerable<Transaction> transactions)
        {
            IEnumerable<Transaction> newTransactions = transactions.Where(x => x.ID == 0);
            IEnumerable<Transaction> updateTransactions = transactions.Where(x => x.ID > 0);

            if (newTransactions.Count() > 0)
                db.Transactions.AddRange(newTransactions);
            if (updateTransactions.Count() > 0)
                db.Transactions.UpdateRange(updateTransactions);

            db.SaveChanges();
        }
    }
}
