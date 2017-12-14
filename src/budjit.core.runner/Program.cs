using budjit.core.data;
using budjit.core.data.Contracts;
using budjit.core.data.SQLite;
using budjit.core.ImportParsers;
using budjit.core.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace budjit.core.runner
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "Examples/CSV/Santander.csv";

            CSVImporter importer = new CSVImporter(new System.IO.FileInfo(filePath));

            List<Transaction> transactions = SantanderCSVParser.Parse(importer).ToList();

            Console.WriteLine($"Found {transactions.Count} transactions");

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fullPath = Path.Combine(path, "SQLite", "budjit.db");

            var builder = new DbContextOptionsBuilder<BudjitContext>().UseSqlite($"DataSource={fullPath}");
            BudjitContext context = new BudjitContext(builder.Options);
            context.Database.Migrate();

            ITransactionsRepository repo = new TransactionRepository(context);
            repo.Create(transactions);

            List<Transaction> databaseTransactions = repo.GetAll().ToList();

            int count = 1;
            foreach (Transaction trans in databaseTransactions)
            {
                Console.WriteLine($"{count} - {trans.Description}");

                count++;
            }

            Console.ReadLine();
        }
    }
}