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

        [TestMethod]
        public void ShouldReturn200OnTagUpdate()
        {
            Transaction transaction = new Transaction() { ID = 1, Description = "Description" };

            var mockTagRepo = new Mock<ITransactionsRepository>();
            mockTagRepo.Setup(x => x.GetById(1)).Returns(transaction);
            mockTagRepo.Setup(x => x.Create(It.IsAny<Transaction>()));

            var controller = new TransactionController(mockTagRepo.Object, Mapper.Instance);

            var actionResult = controller.Post(1, 1);

            Assert.IsNotNull(actionResult);
            OkResult result = actionResult as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void ShouldReturn500OnTagUpdateException()
        {
            Transaction transaction = new Transaction() { ID = 1, Description = "Description" };

            var mockTagRepo = new Mock<ITransactionsRepository>();
            mockTagRepo.Setup(x => x.GetById(1)).Returns(transaction);
            mockTagRepo.Setup(x => x.Create(It.IsAny<Transaction>())).Throws(new Exception("Random exception"));

            var controller = new TransactionController(mockTagRepo.Object, Mapper.Instance);

            var actionResult = controller.Post(1, 1);

            Assert.IsNotNull(actionResult);
            StatusCodeResult result = actionResult as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }
    }
}
