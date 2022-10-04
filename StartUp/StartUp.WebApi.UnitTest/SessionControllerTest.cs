using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using StartUp.Domain;
using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.Models.Models.In;

namespace StartUp.WebApi.UnitTest
{
    [TestClass]
    public class SessionControllerTest
    {
        private Mock<ISessionService> _serviceMock;
        private SessionController _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<ISessionService>(MockBehavior.Strict);
            _controller = new SessionController(_serviceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceMock.VerifyAll();
        }

        [TestMethod]
        public void LogInTest()
        {
            var session = new Session();
            var expectedSession = new SessionDetailModel(session);
            _serviceMock.Setup(manager => manager.GetSpecificSession(It.IsAny<int>())).Returns(session);

            var response = _controller.GetSession(session.Id);
            var okResponseObject = response as OkObjectResult;

            Assert.AreEqual(expectedSession, okResponseObject.Value);
        }
        
    }
}
