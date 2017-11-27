using budjit.core.models;
using System;
using System.Collections.Generic;

namespace budjit.core.data.Contracts
{
    public interface ITransactionsRepository
    {
        Transaction Create(Transaction transaction);
        void Create(IEnumerable<Transaction> transactions);
        Transaction GetById(int id);
        IEnumerable<Transaction> GetInDateRange(DateTime start, DateTime end);
        IEnumerable<Transaction> GetAll();
    }
}
