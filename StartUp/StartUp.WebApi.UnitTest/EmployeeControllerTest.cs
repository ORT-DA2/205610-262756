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
    public class EmployeeControllerTest
    {
        private Mock<IEmployeeService> _managerMock;

        [TestInitialize]
        public void Setup()
        {
            _managerMock = new Mock<IEmployeeService>(MockBehavior.Strict);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _managerMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingEmployeeReturnsAsExpected()
        {
            var employee = CreateEmployee();
            var expectedEmployee = new EmployeeDetailModel(employee);
            _managerMock.Setup(manager => manager.GetSpecificEmployee(It.IsAny<int>())).Returns(employee);
            var controller = new EmployeeController(_managerMock.Object);

            var response = controller.GetEmployee(employee.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedEmployee, okResponseObject.Value);
        }

        private Employee CreateEmployee()
        {
            return new Employee()
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
