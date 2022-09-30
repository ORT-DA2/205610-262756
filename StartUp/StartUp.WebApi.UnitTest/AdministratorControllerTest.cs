using System;
using IBusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.Domain;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Controllers;

namespace StartUp.WebApi.UnitTest
{
    [TestClass]
    public class AdministratorControllerTest
    {
        private Mock<IAdministratorService> _managerMock;
        
        [TestInitialize]
        public void Setup()
        {
            _managerMock = new Mock<IAdministratorService>(MockBehavior.Strict);
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            _managerMock.VerifyAll();
        }
        
        [TestMethod]
        public void GetExistingAdministratorReturnsAsExpected()
        {
            var admin = CreateAdministrator();
            var expectedAdmin = new AdministratorDetailModel(admin);
            _managerMock.Setup(manager => manager.GetSpecificAdministrator(It.IsAny<int>())).Returns(admin);
            var controller = new AdministratorController(_managerMock.Object);

            var response = controller.GetAdministrator(admin.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedAdmin, okResponseObject.Value);
        }
        
        private Administrator CreateAdministrator()
        {
            return new Administrator()
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

