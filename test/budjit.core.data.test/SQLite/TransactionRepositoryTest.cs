using budjit.core.data.Contracts;
using budjit.core.data.SQLite;
using budjit.core.models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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
        private DbContextOptions<BudjitContext> contextOptions;

        public TransactionRepositoryTest()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            DbContextOptions<BudjitContext> options;
            var builder = new DbContextOptionsBuilder<BudjitContext>();
            builder.UseSqlite(connection);
            options = builder.Options;

            contextOptions = options;
        }

        private BudjitContext GetContext(DbContextOptions<BudjitContext> options)
        {
            var context = new BudjitContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            return context;
        }

        [TestMethod]
        public void ShouldSaveTransaction()
        {
            Transaction transaction = new Transaction()
            {
                ID = 100,
                Date = DateTime.Now,
                Description = "Fake Description",
                Merchant = "Fake Merchant",
                Alteration = 100m,
                Balance = 1000m
            };

            using (var context = GetContext(contextOptions))
            {
                ITransactionsRepository transactionsRepository = new TransactionRepository(context);
                
                transactionsRepository.SaveTransaction(transaction);
            }

            Transaction foundTransaction;
            using (var context = GetContext(contextOptions))
            {
                foundTransaction = context.Transactions.Find(transaction.ID);
            }

            Assert.AreEqual(transaction.ID, foundTransaction.ID);
            Assert.AreEqual(transaction.Balance, foundTransaction.Balance);
        }

        [TestMethod]
        public void ShouldSaveTransactions()
        {
            int transactionCount = 10;
            using (var context = GetContext(contextOptions))
            {
                ITransactionsRepository transactionsRepository = new TransactionRepository(context);

                var transactions = Enumerable.Range(1, transactionCount)
                    .Select(i => new Transaction { ID = i, Date = DateTime.Now, Description = $"Fake Description{i}", Merchant = $"Fake Merchant{i}", Alteration = -100m * i, Balance = 1000m * i });

                transactionsRepository.SaveTransactions(transactions);
            }

            int foundTransactionCount;
            using (var context = GetContext(contextOptions))
            {
                foundTransactionCount = context.Transactions.Count();
            }

            Assert.AreEqual(transactionCount, foundTransactionCount);
        }

        [TestMethod]
        public void ShouldGetTransactionById()
        {
            using (var context = GetContext(contextOptions))
            {
                var transactions = Enumerable.Range(1, 10)
                    .Select(i => new Transaction { ID = i, Date = DateTime.Now, Description = $"Fake Description{i}", Merchant = $"Fake Merchant{i}", Alteration = -100m * i, Balance = 1000m * i });

                context.Transactions.AddRange(transactions);
                context.SaveChanges();
            }

            Transaction foundTransaction;
            using (var context = GetContext(contextOptions))
            {
                ITransactionsRepository repository = new TransactionRepository(context);
                foundTransaction = repository.GetTransactionById(1);
            }

            Assert.AreEqual(1, foundTransaction.ID);
        }

        [TestMethod]
        public void ShouldGetTransactionsInDateRange()
        {
            DateTime dateNow = DateTime.Now;
            IEnumerable<Transaction> validTransactions = new List<Transaction> {
                new Transaction { ID = 1, Date = dateNow.AddDays(1), Description = "Fake Description", Merchant = "Fake Merchant", Alteration = -100m, Balance = 1000m },
                new Transaction { ID = 2, Date = dateNow, Description = "Fake Description2", Merchant = "Fake Merchant2", Alteration = -200m, Balance = 2000m },
                new Transaction { ID = 3, Date = dateNow.AddDays(-1), Description = "Fake Description3", Merchant = "Fake Merchant3", Alteration = -300m, Balance = 3000m },
            }.AsQueryable();

            using (var context = GetContext(contextOptions))
            {
                context.Transactions.AddRange(validTransactions);
                context.SaveChanges();
            }

            List<Transaction> foundTransactions;
            using (var context = GetContext(contextOptions))
            {
                ITransactionsRepository repository = new TransactionRepository(context);
                foundTransactions = repository.GetTransactionInDateRange(DateTime.Now.AddDays(-2), DateTime.Now).ToList();
            }

            Assert.AreEqual(2, foundTransactions.Count);
        }

        [TestMethod]
        public void ShouldGetAllTransactions()
        {
            using (var context = GetContext(contextOptions))
            {
                var transactions = Enumerable.Range(1, 10)
                .Select(i => new Transaction { ID = i, Date = DateTime.Now, Description = $"Fake Description{i}", Merchant = $"Fake Merchant{i}", Alteration = -100m * i, Balance = 1000m * i });

                context.Transactions.AddRange(transactions);
                context.SaveChanges();
            }

            List<Transaction> foundTransactions;

            using (var context = GetContext(contextOptions))
            {
                ITransactionsRepository repository = new TransactionRepository(context);
                foundTransactions = repository.GetAll().ToList();
            }
            Assert.AreEqual(10, foundTransactions.Count);
        }
    }
}
