using budjit.core.data;
using budjit.core.data.Contracts;
using budjit.core.data.SQLite;
using budjit.core.ImportParsers;
using budjit.core.models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            
            ITransactionsRepository repo = new TransactionRepository(new BudjitContext());
            repo.SaveTransactions(transactions);

            List<Transaction> databaseTransactions = repo.GetAll().ToList() ;

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