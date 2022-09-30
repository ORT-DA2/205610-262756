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
    public class InvoiceLineControllerTest
    {
        private Mock<IInvoiceLineService> _managerMock;

        [TestInitialize]
        public void Setup()
        {
            _managerMock = new Mock<IInvoiceLineService>(MockBehavior.Strict);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _managerMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingInvoiceLineReturnsAsExpected()
        {
            var invoiceLine = CreateInvoiceLine();
            var expectedInvoiceLine = new InvoiceLineDetailModel(invoiceLine);
            _managerMock.Setup(manager => manager.GetSpecificInvoiceLine(It.IsAny<int>())).Returns(invoiceLine);
            var controller = new InvoiceLineController(_managerMock.Object);

            var response = controller.GetInvoiceLine(invoiceLine.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedInvoiceLine, okResponseObject.Value);
        }

        private InvoiceLine CreateInvoiceLine()
        {
            return new InvoiceLine()
            {
                Medicine = new Medicine(),
                Amount = 0
            };
        }
    }
}
