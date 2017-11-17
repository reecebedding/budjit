using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using budjit.core.ImportParsers;
using System.IO;
using budjit.core.ImportParsers.Contracts;

namespace budjit.core.test.ImportParsers
{
    [TestClass]
    public class CSVImporterTest
    {
        private static string csvData = "Date;Type;Merchant/Description;Debit/Credit;Balance;18 / 10 / 2017; CARD PAYMENT; CARD PAYMENT TO FAKE COMPANY; -£1.95; +£1337.03;";

        [TestMethod]
        public void ShouldImport_FromValidCSVFile()
        {
            string fileLocation = "c:/somerandomfile.csv";
            FileInfo fileInfo = new FileInfo(fileLocation);

            var mockFileAccess = new Mock<IFileAccess>();
            mockFileAccess.Setup(x => x.Exists(fileInfo)).Returns(true);
            mockFileAccess.Setup(x => x.ReadAllText(fileInfo)).Returns(csvData);
            var fileAccess = mockFileAccess.Object;
            
            CSVImporter importer = new CSVImporter(fileInfo, fileAccess);
            var result = importer.Import();

            Assert.AreEqual<string>(csvData, result);
            mockFileAccess.Verify(fa => fa.Exists(fileInfo), Times.AtLeastOnce());
            mockFileAccess.Verify(fa => fa.ReadAllText(fileInfo), Times.Once());
        }

        [TestMethod]
        
        public void ShouldThrowException_WhenFileNotPresent()
        {
            string fileLocation = "c:/somerandomfile.csv";
            FileInfo fileInfo = new FileInfo(fileLocation);

            var mockFileAccess = new Mock<IFileAccess>();
            mockFileAccess.Setup(x => x.Exists(fileInfo)).Returns(false);
            var fileAccess = mockFileAccess.Object;

            CSVImporter importer = new CSVImporter(fileInfo, fileAccess);
            
            Assert.ThrowsException<FileNotFoundException>(importer.Import);
            mockFileAccess.Verify(fa => fa.Exists(fileInfo), Times.AtLeastOnce());
            mockFileAccess.Verify(fa => fa.ReadAllText(fileInfo), Times.Never());
        }
    }
}
