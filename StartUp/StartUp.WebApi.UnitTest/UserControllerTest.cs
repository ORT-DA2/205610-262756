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
using StartUp.Domain;
using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.Models.Models.In;

namespace StartUp.WebApi.UnitTest
{
    [TestClass]
    public class UserControllerTest
    {
        private Mock<IUserService> _serviceMock;
        private UserController _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IUserService>(MockBehavior.Strict);
            _controller = new UserController(_serviceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingUserTest()
        {
            var user = CreateUser();
            var expectedUser = new UserDetailModel(user);
            _serviceMock.Setup(manager => manager.GetSpecificUser(It.IsAny<int>())).Returns(user);

            var response = _controller.GetUser(user.Id);
            var okResponseObject = response as OkObjectResult;

            Assert.AreEqual(expectedUser, okResponseObject.Value);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetNotExistingUserTest()
        {
            User usr = CreateUser();
            _serviceMock.Setup(manager => manager.GetSpecificUser(It.IsAny<int>())).Returns((User)null);

            _controller.GetUser(usr.Id);
        }
        
        [TestMethod]
        public void GetExistingUserWithModelTest()
        {
            UserSearchCriteriaModel userModel = new UserSearchCriteriaModel();
            userModel.Email = "hola@gmail.com";
            userModel.Address = "Solano Garcia";
            userModel.Roles = new Role
            {
                Id = 1,
                Permission = "Administrator"
            };
            List<User> usrList = new List<User>();
            User usr = CreateUser();
            usrList.Add(usr);
            
            _serviceMock.Setup(manager => manager.GetAllUser(It.IsAny<UserSearchCriteria>())).Returns(usrList);

            _controller.GetUserByRol(userModel);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetNotExistingUserWithModelTest()
        {
            UserSearchCriteriaModel userModel = new UserSearchCriteriaModel();
            var exception = new ResourceNotFoundException("No users where found");

            _serviceMock.Setup(manager => manager.GetAllUser(It.IsAny<UserSearchCriteria>())).Throws(exception);

            var response = _controller.GetUserByRol(userModel);
        }
        
        [TestMethod]
        public void CreateUserTest()
        {
            UserModel userModel = CreateUserModel();
            User user = CreateUser();
            _serviceMock.Setup(server => server.CreateUser(It.IsAny<User>())).Returns(user);

            var response = _controller.CreateUser(userModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.Created, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void UpdateUserTest()
        {
            UserModel userModel = CreateUserModel();
            User user = CreateUser();
            userModel.Address = "Minas";
            _serviceMock.Setup(server => server.UpdateUser(user.Id, userModel.ToEntity())).Returns(userModel.ToEntity);

            var response = _controller.Update(user.Id, userModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void DeleteUserTest()
        {
            User user = CreateUser();
            _serviceMock.Setup(server => server.DeleteUser(user.Id));

            var response = _controller.Delete(user.Id);
            var okResponseObject = response as OkResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }

        private User CreateUser()
        {
            return new User()
            {
                Id = 1,
                Address = "Guayaqui",
                Email = "Chau@gmail.com",
                Password = "AS12345K!",
            };
        }
        
        private UserModel CreateUserModel()
        {
            return new UserModel()
            {
                Address = "Guayaqui",
                Email = "Chau@gmail.com",
                Password = "AS12345K!",
            };
        }
    }
}