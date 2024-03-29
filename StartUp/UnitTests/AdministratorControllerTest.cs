﻿using IBusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.Domain;
using StartUp.Models.Models.Out;
using System;
using WebApi.Controllers;

namespace StartUp.WebApi.UnitTests
{
    [TestClass]
    public class AdministratorControllerTest
    {
        private Mock<IAdministratorManager> _managerMock;

        [TestInitialize]
        public void Setup()
        {
            _managerMock = new Mock<IAdministratorManager>(MockBehavior.Strict);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _managerMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingAdministratorReturnsAsExpected()
        {
            // 1. Arrange
            var admin = CreateAdministrator();
            var expectedAdmin = new AdministratorDetailModel(admin);
            _managerMock.Setup(manager => manager.GetSpecificAdministrator(It.IsAny<string>())).Returns(admin);
            var controller = new AdministratorController(_managerMock.Object);

            // 2. Act 
            var response = controller.GetAdministrator(admin.Email);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            // 3. Assert
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

