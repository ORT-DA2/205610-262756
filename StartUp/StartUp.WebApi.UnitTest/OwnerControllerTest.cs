using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.Domain;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Controllers;
using System;

namespace StartUp.WebApi.UnitTest
{
    [TestClass]
    public class OwnerControllerTest
    {
        private Mock<IOwnerService> _managerMock;

        [TestInitialize]
        public void Setup()
        {
            _managerMock = new Mock<IOwnerService>(MockBehavior.Strict);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _managerMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingOwnerReturnsAsExpected()
        {
            var owner = CreateOwner();
            var expectedOwner = new OwnerDetailModel(owner);
            _managerMock.Setup(manager => manager.GetSpecificOwner(It.IsAny<int>())).Returns(owner);
            var controller = new OwnerController(_managerMock.Object);

            var response = controller.GetOwner(owner.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedOwner, okResponseObject.Value);
        }

        private Owner CreateOwner()
        {
            return new Owner()
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
