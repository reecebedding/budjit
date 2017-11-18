using budjit.core.data.SQLite;
using budjit.core.data.SQLite.Formatters;
using budjit.core.models;
using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [TestMethod]
        public void ShouldGetTransactionById()
        {
            var mockContext = new Mock<IBudjitContext>();
            mockContext.Setup(x => x.Query(It.IsAny<SqliteCommand>(), It.IsAny<TransactionDataFormatter>())).Returns(new List<object>()
            {
                new Transaction(){ Balance = 100 }
            });
            var context = mockContext.Object;

            TransactionRepository repository = new TransactionRepository(context);
            Transaction result = repository.GetTransactionById(1);

            Assert.AreEqual(100, result.Balance);
            mockContext.Verify(x => x.Query(It.IsAny<SqliteCommand>(), It.IsAny<TransactionDataFormatter>()), Times.Once());
        }

        [TestMethod]
        public void ShouldGetTransactionsInRange()
        {
            var mockContext = new Mock<IBudjitContext>();
            mockContext.Setup(x => x.Query(It.IsAny<SqliteCommand>(), It.IsAny<TransactionDataFormatter>())).Returns(new List<object>()
            {
                new Transaction(){ Balance = 100 },
                new Transaction(){ Balance = 123 },
                new Transaction(){ Balance = 456 },
            });
            var context = mockContext.Object;

            TransactionRepository repository = new TransactionRepository(context);
            List<Transaction> result = repository.GetTransactionInDateRange(DateTime.Parse("2017-10-11 00:00:00"), DateTime.Parse("2017-10-16 00:00:00")).ToList(); 

            Assert.AreEqual(3, result.Count);
            mockContext.Verify(x => x.Query(It.IsAny<SqliteCommand>(), It.IsAny<TransactionDataFormatter>()), Times.Once());
        }

        [TestMethod]
        public void ShouldGetAllTransactions()
        {
            var mockContext = new Mock<IBudjitContext>();
            mockContext.Setup(x => x.Query(It.IsAny<SqliteCommand>(), It.IsAny<TransactionDataFormatter>())).Returns(new List<object>()
            {
                new Transaction(){ Balance = 100 },
                new Transaction(){ Balance = 123 },
                new Transaction(){ Balance = 456 },
            });
            var context = mockContext.Object;

            TransactionRepository repository = new TransactionRepository(context);
            List<Transaction> result = repository.GetAll().ToList();

            Assert.AreEqual(3, result.Count);
            mockContext.Verify(x => x.Query(It.IsAny<SqliteCommand>(), It.IsAny<TransactionDataFormatter>()), Times.Once());
        }
    }
}
