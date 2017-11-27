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

        public Transaction GetById(int id)
        {
            return db.Transactions
                .Include(trans => trans.Tag)
                .Where(x => x.ID == id).FirstOrDefault();
        }

        public IEnumerable<Transaction> GetInDateRange(DateTime start, DateTime end)
        {
            return db.Transactions
                .Include(trans => trans.Tag)
                .Where(x => (x.Date >= start && x.Date <= end));
        }

        public Transaction Create(Transaction transaction)
        {
            transaction = db.Transactions.Add(transaction).Entity;
            db.SaveChanges();

            return transaction;
        }

        public void Create(IEnumerable<Transaction> transactions)
        {
            db.Transactions.AddRange(transactions);

            db.SaveChanges();
        }
    }
}
