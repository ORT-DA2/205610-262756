using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.Domain;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.Models.Models.In;

namespace StartUp.WebApi.UnitTest
{
    [TestClass]
    public class RequestControllerTest
    {
        private Mock<IRequestService> _serviceMock;
        private RequestController _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IRequestService>(MockBehavior.Strict);
            _controller = new RequestController(_serviceMock.Object);

        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingRequestReturnsAsExpected()
        {
            var request = CreateRequest();
            var expectedRequest = new RequestDetailModel(request);
            _serviceMock.Setup(service => service.GetSpecificRequest(It.IsAny<int>())).Returns(request);
            var controller = new RequestController(_serviceMock.Object);

            var response = controller.GetRequest(request.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedRequest, okResponseObject.Value);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetNotExistingRequestTest()
        {
            Request inv = CreateRequest();
            _serviceMock.Setup(service => service.GetSpecificRequest(It.IsAny<int>())).Returns((Request)null);

            _controller.GetRequest(inv.Id);
        }
        
        [TestMethod]
        public void GetExistingRequestWithModelTest()
        {
            RequestSearchCriteriaModel requestModel = new RequestSearchCriteriaModel();
            List<Request> invList = new List<Request>();
            Request inv = CreateRequest();
            invList.Add(inv);
            
            _serviceMock.Setup(service => service.GetAllRequest(It.IsAny<RequestSearchCriteria>())).Returns(invList);

            _controller.GetRequest(requestModel);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetNotExistingRequestWithModelTest()
        {
            RequestSearchCriteriaModel requestModel = new RequestSearchCriteriaModel();
            var exception = new ResourceNotFoundException("No requests where found");

            _serviceMock.Setup(service => service.GetAllRequest(It.IsAny<RequestSearchCriteria>())).Throws(exception);

            var response = _controller.GetRequest(requestModel);
        }
        
        [TestMethod]
        public void CreateRequestTest()
        {
            RequestModel requestModel = CreateRequestModel();
            Request request = CreateRequest();
            _serviceMock.Setup(server => server.CreateRequest(It.IsAny<Request>())).Returns(request);

            var response = _controller.CreateRequest(requestModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.Created, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void UpdateRequestTest()
        {
            RequestModel requestModel = CreateRequestModel();
            Request request = CreateRequest();
            _serviceMock.Setup(server => server.UpdateRequest(request.Id, requestModel.ToEntity())).Returns(requestModel.ToEntity);

            var response = _controller.Update(request.Id, requestModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void DeleteRequestTest()
        {
            Request request = CreateRequest();
            _serviceMock.Setup(server => server.DeleteRequest(request.Id));

            var response = _controller.Delete(request.Id);
            var okResponseObject = response as OkResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }

        private Request CreateRequest()
        {
            return new Request()
            {
                Petitions = new List<Petition>(),
            };
        }
    }
}
