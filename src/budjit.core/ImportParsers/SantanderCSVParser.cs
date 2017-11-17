using budjit.core.ImportParsers.Contracts;
using System;
using System.Collections.Generic;
using budjit.core.models;
using System.Linq;
using System.Globalization;
using budjit.core.ImportParsers.Exceptions;

namespace budjit.core.ImportParsers
{
    public class SantanderCSVParser : IImportParser
    {
        private static char delimiter = ',';
        private static char terminator = ';';

        private IImporter importer;
        public SantanderCSVParser(IImporter importer)
        {
            this.importer = importer;
        }

        private static string GetDataFromImporter(IImporter importer)
        {
            return importer.Import();   
        }

        public static IEnumerable<Transaction> Parse(IImporter importer)
        {
            string data = GetDataFromImporter(importer);
            return Parse(data);
        }

        public IEnumerable<Transaction> Parse()
        {
            string data = GetDataFromImporter(importer);
            return Parse(data);
        }

        public static IEnumerable<Transaction> Parse(string data)
        {
            List<Transaction> transactions = new List<Transaction>();
            int columnCount = 5;

            string[] lines = data.Split(terminator);
            lines = lines.Skip(columnCount).ToArray();

            if (lines.Length <= 0)
                throw new InvalidCSVException("The CSV Content is empty");

            string[] currentTransaction = new string[5];
            int currentVal = 0;

            try
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i % 5 == 0 && i > 0)
                    {
                        Transaction transaction = new Transaction()
                        {
                            Date = DateTime.Parse(currentTransaction[0]),
                            Description = currentTransaction[2],
                            Merchant = currentTransaction[2],
                            Alteration = decimal.Parse(currentTransaction[3], NumberStyles.Currency),
                            Balance = decimal.Parse(currentTransaction[4], NumberStyles.Currency)
                        };
                        transactions.Add(transaction);
                        currentTransaction = new string[5];
                        currentVal = 0;
                    }

                    currentTransaction[currentVal] = lines[i];
                    currentVal++;
                }
                return transactions;
            }
            catch (Exception e)
            {
                throw new InvalidCSVException($"Error parsing CSV data. Error: {e.Message}");
            }
        }
    }
}
