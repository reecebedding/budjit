using budjit.core.data.Contracts;
using budjit.core.models;
using budjit.ui.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using budjit.ui.API.ViewModel;

namespace budjit.ui.test.API
{
    [TestClass]
    public class TransactionControllerTest
    {
        [TestMethod]
        public void ShouldReturnOkForGetAll()
        {
            int transactionCount = 10;
            IEnumerable<Transaction> transactions = Enumerable.Range(1, transactionCount)
                .Select(x => new Transaction() { ID = x, Merchant = $"Transaction {x}" });

            var mockTransactionRepo = new Mock<ITransactionsRepository>();
            mockTransactionRepo.Setup(x => x.GetAll()).Returns(transactions);

            var controller = new TransactionController(mockTransactionRepo.Object, Mapper.Instance);

            var result = controller.GetAll();
            var okResult = result as OkObjectResult;
            var content = okResult.Value as IEnumerable<TransactionViewModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual(transactionCount, content.Count());
        }

        [TestMethod]
        public void GetAllShouldReturnServerErrorForFault()
        {
            int transactionCount = 10;
            IEnumerable<Transaction> transactions = Enumerable.Range(1, transactionCount)
                .Select(x => new Transaction() { ID = x, Merchant = $"Transaction {x}" });

            var mockTransactionRepo = new Mock<ITransactionsRepository>();
            mockTransactionRepo.Setup(x => x.GetAll()).Throws(new Exception("Server error in repository"));

            var controller = new TransactionController(mockTransactionRepo.Object, Mapper.Instance);

            var result = controller.GetAll();
            var errorResult = result as StatusCodeResult;

            Assert.IsNotNull(errorResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
    }
}
