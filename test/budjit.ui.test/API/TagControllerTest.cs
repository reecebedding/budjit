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
using AutoMapper;
using budjit.ui.API.ViewModel;
using System;

namespace budjit.ui.test
{
    [TestClass]
    public class TagControllerTest
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            Mapper.Initialize(m => m.AddProfile<AutoMapperProfile>());
        }

        [TestMethod]
        public void ShouldGetAllTags()
        {
            int tagCount = 10;
            IEnumerable<Tag> tags = Enumerable.Range(1, tagCount)
                .Select(x => new Tag() { ID = x, Name = $"Tag {x}" });

            var mockTagRepo = new Mock<ITagRepository>();
            mockTagRepo.Setup(x => x.GetAll()).Returns(tags);

            var controller = new TagController(mockTagRepo.Object, Mapper.Instance);

            var result = controller.GetAllTags();
            var okResult = result as OkObjectResult;
            var content = okResult.Value as IEnumerable<TagViewModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual(tagCount, content.Count());
        }

        [TestMethod]
        public void GetAllTagsShouldReturnServerErrorForFault()
        {
            int tagCount = 10;
            IEnumerable<Tag> tags = Enumerable.Range(1, tagCount)
                .Select(x => new Tag() { ID = x, Name = $"Tag {x}" });

            var mockTagRepo = new Mock<ITagRepository>();
            mockTagRepo.Setup(x => x.GetAll()).Throws(new Exception("Server error in repository"));

            var controller = new TagController(mockTagRepo.Object, Mapper.Instance);

            var result = controller.GetAllTags();
            var errorResult = result as StatusCodeResult;

            Assert.IsNotNull(errorResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }

        [TestMethod]
        public void ShouldGetById()
        {
            Tag tag = new Tag() { ID = 1, Name = "Tag 1" };

            var mockTagRepo = new Mock<ITagRepository>();
            mockTagRepo.Setup(x => x.GetById(1)).Returns(tag);

            var controller = new TagController(mockTagRepo.Object, Mapper.Instance);

            var result = controller.GetById(1);
            var okResult = result as OkObjectResult;
            var content = okResult.Value as TagViewModel;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            Assert.AreEqual(tag.ID, content.ID);
            Assert.AreEqual(tag.Name, content.Name);
            mockTagRepo.Verify(x => x.GetById(1), Times.Once);
        }

        [TestMethod]
        public void GetByIdShouldReturnServerErrorForFault()
        {
            Tag tag = new Tag() { ID = 1, Name = "Tag 1" };

            var mockTagRepo = new Mock<ITagRepository>();
            mockTagRepo.Setup(x => x.GetById(It.IsAny<int>())).Throws(new Exception("Server error in repository"));

            var controller = new TagController(mockTagRepo.Object, Mapper.Instance);

            var result = controller.GetById(1);
            var errorResult = result as StatusCodeResult;

            Assert.IsNotNull(errorResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }

        [TestMethod]
        public void ShouldCreateNewTags()
        {
            TagViewModel newTagVM = new TagViewModel() { Name = "Tag 1" };
            Tag newTag = new Tag() { Name = "Tag 1" };

            var mockTagRepo = new Mock<ITagRepository>();
            mockTagRepo.Setup(x => x.Create(It.IsAny<Tag>())).Returns(newTag);
            var controller = new TagController(mockTagRepo.Object, Mapper.Instance);

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
            var actionResult = controller.Create(newTagVM);

            Assert.IsNotNull(actionResult);
            CreatedResult result = actionResult as CreatedResult;

            Assert.AreEqual("/api/tag/1", result.Location);
            mockTagRepo.Verify(x => x.Create(It.IsAny<Tag>()), Times.Once);
        }

        [TestMethod]
        public void CreateNewTagsShouldReturnBadRequestOnMissingTagName()
        {
            TagViewModel newTag = new TagViewModel() { };
            var mockRepository = new Mock<ITagRepository>();

            var controller = new TagController(mockRepository.Object, Mapper.Instance);
            controller.ModelState.AddModelError("NameMissing", "Missing");

            var actionResult = controller.Create(newTag);

            Assert.IsNotNull(actionResult);
            BadRequestObjectResult result = actionResult as BadRequestObjectResult;
        }

        [TestMethod]
        public void CreateNewTagsShouldReturnServerErrorOnFault()
        {
            TagViewModel newTag = new TagViewModel() { };
            var mockRepository = new Mock<ITagRepository>();
            mockRepository.Setup(x => x.Create(It.IsAny<Tag>())).Throws(new Exception("Server error in repository"));

            var controller = new TagController(mockRepository.Object, Mapper.Instance);

            var actionResult = controller.Create(newTag);
            var errorResult = actionResult as StatusCodeResult;

            Assert.IsNotNull(errorResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        }
    }
}
