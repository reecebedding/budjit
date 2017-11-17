using budjit.core.models;
using System.Collections.Generic;

namespace budjit.core.data.Contracts
{
    public interface ITransactionsRepository
    {
        void SaveTransaction(Transaction transaction);
        void SaveTransactions(IEnumerable<Transaction> transactions);
    }
}
