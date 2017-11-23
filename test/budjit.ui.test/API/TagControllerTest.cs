using budjit.core.data.Contracts;
using budjit.core.models;
using budjit.ui.API;
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
    }
}
