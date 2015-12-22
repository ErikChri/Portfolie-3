using NUnit.Framework;
using ProductApp.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ProductApp.Controllers;
using System.Web.Http.Routing;

namespace ProductApp.Tests
{
    [TestFixture]
    class ModelFactoryTests
    {
        [Test]
        public void CreatePostJsonModelTest()
        {
            var urlHelperMock = new Mock<UrlHelper>();
           
            var post = new Post { _title = "Testing Posting a post", _Qi_ID = 123 };

            var jsonModel = ModelFactory.Create(post, urlHelperMock.Object);

            Assert.AreEqual(post._title, jsonModel.title);

            urlHelperMock.Verify(m => m.Link("PostApi", It.IsAny<object>()));
        }

        
    }
}
