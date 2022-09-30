using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.Domain;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.WebApi.UnitTest
{
    [TestClass]
    public class RequestControllerTest
    {
        private Mock<IRequestService> _managerMock;

        [TestInitialize]
        public void Setup()
        {
            _managerMock = new Mock<IRequestService>(MockBehavior.Strict);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _managerMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingRequestReturnsAsExpected()
        {
            var request = CreateRequest();
            var expectedRequest = new RequestDetailModel(request);
            _managerMock.Setup(manager => manager.GetSpecificRequest(It.IsAny<int>())).Returns(request);
            var controller = new RequestController(_managerMock.Object);

            var response = controller.GetRequest(request.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedRequest, okResponseObject.Value);
        }

        private Request CreateRequest()
        {
            return new Request()
            {
                Petitions = new List<Petition>(),
                State = true
            };
        }
    }
}
