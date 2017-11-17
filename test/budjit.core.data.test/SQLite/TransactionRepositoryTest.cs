using budjit.core.data.SQLite;
using budjit.core.models;
using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace budjit.core.data.test.SQLite
{
    [TestClass]
    public class TransactionRepositoryTest
    {
        [TestMethod]
        public void ShouldSaveTransaction()
        {
            var mockContext = new Mock<IBudjitContext>();
            mockContext.Setup(x => x.Save(It.IsAny<SqliteCommand>()));
            var context = mockContext.Object;

            Transaction transaction = new Transaction();

            TransactionRepository repository = new TransactionRepository(context);
            repository.SaveTransaction(transaction);

            mockContext.Verify(x => x.Save(It.IsAny<SqliteCommand>()), Times.Once());
        }

        [TestMethod]
        public void ShouldSaveTransactions()
        {
            var mockContext = new Mock<IBudjitContext>();
            mockContext.Setup(x => x.Save(It.IsAny<IEnumerable<SqliteCommand>>()));
            var context = mockContext.Object;

            List<Transaction> transactions = new List<Transaction>();
            for (int i = 0; i < 54; i++)
            {
                transactions.Add(new Transaction());
            }

            TransactionRepository repository = new TransactionRepository(context);
            repository.SaveTransactions(transactions);

            mockContext.Verify(x => x.Save(It.IsAny<IEnumerable<SqliteCommand>>()), Times.Once());
        }
    }
}
