using IBusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.Domain;
using StartUp.Domain.Entities;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Controllers;
using System;

namespace StartUp.WebApi.UnitTest
{
    public class UserControllerTest
    {
        private Mock<IUserService> _managerMock;

        [TestInitialize]
        public void Setup()
        {
            _managerMock = new Mock<IUserService>(MockBehavior.Strict);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _managerMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingUserReturnsAsExpected()
        {
            var user = CreateUser();
            var expectedUser = new UserDetailModel(user);
            _managerMock.Setup(manager => manager.GetSpecificUser(It.IsAny<int>())).Returns(user);
            var controller = new UserController(_managerMock.Object);

            var response = controller.GetUser(user.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedUser, okResponseObject.Value);
        }

        private User CreateUser()
        {
            return new User()
            {
                Email = "email1@gmail.com",
                Password = "123456",
                RegisterDate = DateTime.Now,
                Address = "Carlos maria ramirez 105",
                Invitation = new Invitation
                {
                    Code = 1236,
                    Rol = "Administrator",
                    UserName = "apodo"
                }
            };
        }
    }
}
