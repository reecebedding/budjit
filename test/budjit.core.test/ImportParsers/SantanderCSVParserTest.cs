using budjit.core.ImportParsers;
using budjit.core.ImportParsers.Contracts;
using budjit.core.ImportParsers.Exceptions;
using budjit.core.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace budjit.core.test.ImportParsers
{
    [TestClass]
    public class SantanderCSVParserTest
    {
        private static string validCsvData = "Date;Type;Merchant/Description;Debit/Credit;Balance;18 / 10 / 2017; CARD PAYMENT; CARD PAYMENT TO FAKE COMPANY; -£1.95; +£1337.03;";
        private static string validMultipleCsvData = "Date;Type;Merchant/Description;Debit/Credit;Balance;18 / 10 / 2017; CARD PAYMENT; CARD PAYMENT TO FAKE COMPANY; -£1.95; +£1337.03;19 / 11 / 2018; CARD PAYMENT; CARD PAYMENT TO FAKE COMPANY 2; +£10.30; -£123.45;";

        [TestMethod]
        public void ShouldParseCSV_ForValidCSV()
        {
            var mockImporter = new Mock<IImporter>();
            mockImporter.Setup(x => x.Import()).Returns(validCsvData);
            var importer = mockImporter.Object;

            SantanderCSVParser parser = new SantanderCSVParser(importer);
            List<Transaction> result = parser.Parse().ToList();

            Assert.AreEqual(1, result.Count);

            AssertTransactionValues(new DateTime(2017, 10, 18), " CARD PAYMENT TO FAKE COMPANY", " CARD PAYMENT TO FAKE COMPANY", -1.95m, 1337.03m, result.First());
        }

        [TestMethod]
        public void ShouldParseCSV_ForValidCSVWithMultipleTransactions()
        {
            var mockImporter = new Mock<IImporter>();
            mockImporter.Setup(x => x.Import()).Returns(validMultipleCsvData);
            var importer = mockImporter.Object;

            SantanderCSVParser parser = new SantanderCSVParser(importer);
            Transaction[] result = parser.Parse().ToArray();

            Assert.AreEqual(2, result.Length);

            AssertTransactionValues(new DateTime(2017, 10, 18), " CARD PAYMENT TO FAKE COMPANY", " CARD PAYMENT TO FAKE COMPANY", -1.95m, 1337.03m, result[0]);
            AssertTransactionValues(new DateTime(2018, 11, 19), " CARD PAYMENT TO FAKE COMPANY 2", " CARD PAYMENT TO FAKE COMPANY 2", 10.30m, -123.45m, result[1]);
        }

        [TestMethod]
        public void ShouldThrowException_ForEmptyCSV()
        {
            var mockImporter = new Mock<IImporter>();
            mockImporter.Setup(x => x.Import()).Returns("");
            var importer = mockImporter.Object;

            SantanderCSVParser parser = new SantanderCSVParser(importer);

            Assert.ThrowsException<InvalidCSVException>(parser.Parse, "The CSV Content is empty");
            mockImporter.Verify(i => i.Import(), Times.Once());
        }

        [TestMethod]
        public void ShouldThrowException_ForInvalidCSV()
        {
            var mockImporter = new Mock<IImporter>();
            mockImporter.Setup(x => x.Import()).Returns("Date;Type;Merchant/Description;Debit/Credit;Balance;18 / 10 / 2017;RANDOM;RANDOM;NOTANUMBER;NOTANUMBER;RANDOM");
            var importer = mockImporter.Object;

            SantanderCSVParser parser = new SantanderCSVParser(importer);

            Assert.ThrowsException<InvalidCSVException>(parser.Parse, "Error parsing CSV data. Error: Input string was not in a correct format.");
            mockImporter.Verify(i => i.Import(), Times.Once());
        }

        private void AssertTransactionValues(DateTime date, string merchant, string description, decimal alteration, decimal balance, Transaction actual)
        {
            Assert.AreEqual(date, actual.Date);
            Assert.AreEqual(merchant, actual.Merchant);
            Assert.AreEqual(description, actual.Description);
            Assert.AreEqual(alteration, actual.Alteration);
            Assert.AreEqual(balance, actual.Balance);
        }
    }
}
