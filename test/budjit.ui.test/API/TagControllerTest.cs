using budjit.core.data.Contracts;
using budjit.core.models;
using budjit.ui.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace budjit.ui.test
{
    [TestClass]
    public class TagControllerTest
    {
        [TestMethod]
        public void ShouldGetAllTags()
        {
            int tagCount = 10;
            IEnumerable<Tag> tags = Enumerable.Range(1, tagCount)
                .Select(x => new Tag() { ID = x, Name = $"Tag {x}" });

            var mockTagRepo = new Mock<ITagRepository>();
            mockTagRepo.Setup(x => x.GetAll()).Returns(tags);

            var controller = new TagController(mockTagRepo.Object);
            var result = controller.GetAllTags();

            Assert.AreEqual(tagCount, result.Count());
        }

        [TestMethod]
        public void ShouldGetById()
        {
            Tag tag = new Tag() { ID = 1, Name = "Tag 1" };

            var mockTagRepo = new Mock<ITagRepository>();
            mockTagRepo.Setup(x => x.GetById(1)).Returns(tag);

            var controller = new TagController(mockTagRepo.Object);
            var result = controller.GetById(1);

            Assert.AreEqual(tag.ID, result.ID);
            Assert.AreEqual(tag.Name, result.Name);
            mockTagRepo.Verify(x => x.GetById(1), Times.Once);
        }

        [TestMethod]
        public void ShouldCreateNewTags()
        {
            Tag newTag = new Tag() { Name = "Tag 1" };

            var mockTagRepo = new Mock<ITagRepository>();
            mockTagRepo.Setup(x => x.Create(newTag)).Returns(newTag);
            var controller = new TagController(mockTagRepo.Object);

            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);
            mockUrlHelper
                .Setup(
                    x => x.Action(
                        It.IsAny<UrlActionContext>()
                    )
                )
                .Returns("/api/tag/1")
                .Verifiable();

            controller.Url = mockUrlHelper.Object;
            var actionResult = controller.Create(newTag);

            Assert.IsNotNull(actionResult);
            CreatedResult result = actionResult as CreatedResult;

            Assert.AreEqual("/api/tag/1", result.Location);
            mockTagRepo.Verify(x => x.Create(newTag), Times.Once);
        }

        [TestMethod]
        public void ShouldReturnBadRequestOnMissingTagName()
        {
            Tag newTag = new Tag() { Name = null };
            var mockRepository = new Mock<ITagRepository>();

            var controller = new TagController(mockRepository.Object);
            var actionResult = controller.Create(newTag);

            Assert.IsNotNull(actionResult);
            BadRequestObjectResult result = actionResult as BadRequestObjectResult;

            Assert.AreEqual("Name cannot be empty", result.Value);
        }
    }
}
