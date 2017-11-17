using budjit.core.ImportParsers;
using budjit.core.Models;
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

            int count = 1;
            foreach (Transaction trans in transactions)
            {
                Console.WriteLine($"{count} - {trans.Description}");
                count++;       
            }

            Console.ReadLine();
        }
    }
}