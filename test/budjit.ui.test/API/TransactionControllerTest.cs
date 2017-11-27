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

namespace budjit.ui.test.API
{
    [TestClass]
    public class TransactionControllerTest
    {
        [TestMethod]
        public void ShouldReturn200OnTagUpdate()
        {
            Transaction transaction = new Transaction() { ID = 1, Description = "Description" };

            var mockTagRepo = new Mock<ITransactionsRepository>();
            mockTagRepo.Setup(x => x.GetById(1)).Returns(transaction);
            mockTagRepo.Setup(x => x.Create(It.IsAny<Transaction>()));

            var controller = new TransactionController(mockTagRepo.Object);

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

            var controller = new TransactionController(mockTagRepo.Object);

            var actionResult = controller.Post(1, 1);

            Assert.IsNotNull(actionResult);
            StatusCodeResult result = actionResult as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }
    }
}
