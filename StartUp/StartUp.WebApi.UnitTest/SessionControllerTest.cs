using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Net;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Expressions;
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
        private Mock<ITokenAccessService> _tokenServiceMock;
        private Mock<IUserService> _userServiceMock;
        private SessionController _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<ISessionService>(MockBehavior.Strict);
            _tokenServiceMock = new Mock<ITokenAccessService>(MockBehavior.Strict);
            _userServiceMock = new Mock<IUserService>(MockBehavior.Strict);
            _controller = new SessionController(_serviceMock.Object, _tokenServiceMock.Object, _userServiceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceMock.VerifyAll();
            _tokenServiceMock.VerifyAll();
            _userServiceMock.VerifyAll();
        }

        [TestMethod]
        public void LogInTest()
        {
            var session = new Session();
            var user = new User();
            user.Id = session.Id;
            user.Password = session.Password;
            TokenAccess token = new TokenAccess();
            token.Token = new Guid();
            var expectedSession = new SessionModel(session);
            _serviceMock.Setup(manager => manager.VerifySession(expectedSession)).Returns(user);
            _serviceMock.Setup(manager => manager.CreateOrRetrieveSession(expectedSession)).Returns(session);
            _tokenServiceMock.Setup(tService => tService.GetSpecificTokenAccess(session)).Returns(token);
            _userServiceMock.Setup(uService => uService.SaveToken(user, token.Token.ToString()));
            
            var response = _controller.Login(expectedSession);
            var responseObj = response.Result as CreatedResult;

            Assert.AreEqual((int)HttpStatusCode.Created, responseObj.StatusCode);
        }
        
        [TestMethod]
        public void LogInFailTest()
        {
            var session = new Session();
            var user = new User();
            user.Id = session.Id;
            user.Password = session.Password;
            TokenAccess token = new TokenAccess();
            token.Token = new Guid();
            var expectedSession = new SessionModel(session);
            _serviceMock.Setup(manager => manager.VerifySession(expectedSession)).Returns(user);
            _serviceMock.Setup(manager => manager.CreateOrRetrieveSession(expectedSession)).Returns(session);
            _tokenServiceMock.Setup(tService => tService.GetSpecificTokenAccess(session)).Returns(token);
            _userServiceMock.Setup(uService => uService.SaveToken(user, token.Token.ToString()));
            
            var response = _controller.Login(expectedSession);
            var responseObj = response.Result as CreatedResult;

            Assert.AreEqual((int)HttpStatusCode.Created, responseObj.StatusCode);
        }
        
        [TestMethod]
        public void LogOutTest()
        {
            TokenAccess token = new TokenAccess();
            token.Token = new Guid();
            
            var user = new User();
            user.Id = 1;
            user.Password = "password12!";
            user.Invitation = new Invitation
            {
                UserName = "Juan"
            };
            user.Token = token.ToString();
            
            var session = new Session();
            session.Username = user.Invitation.UserName;
            session.Password = user.Password;
            session.Id = user.Id;

            var expectedSession = new SessionModel(session);

            _serviceMock.Setup(manager => manager.GetSpecificSession(user.Invitation.UserName)).Returns(session);
            _serviceMock.Setup(manager => manager.VerifySession(expectedSession)).Returns(user);
            _userServiceMock.Setup(uService => uService.SaveToken(user, token.Token.ToString()));
            
            var response = _controller.Logout(user.Token,expectedSession.UserName);
            var responseObj = response as CreatedResult;

            Assert.AreEqual((int)HttpStatusCode.OK, responseObj.StatusCode);
        }
    }
}
