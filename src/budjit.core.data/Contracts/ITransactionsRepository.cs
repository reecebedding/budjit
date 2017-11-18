using budjit.core.models;
using System;
using System.Collections.Generic;

namespace budjit.core.data.Contracts
{
    public interface ITransactionsRepository
    {
        void SaveTransaction(Transaction transaction);
        void SaveTransactions(IEnumerable<Transaction> transactions);
        Transaction GetTransactionById(int id);
        IEnumerable<Transaction> GetTransactionInDateRange(DateTime start, DateTime end);
        IEnumerable<Transaction> GetAll();
    }
}
