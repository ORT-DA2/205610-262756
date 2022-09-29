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
    public class SaleControllerTest
    {
        private Mock<ISaleService> _managerMock;

        [TestInitialize]
        public void Setup()
        {
            _managerMock = new Mock<ISaleService>(MockBehavior.Strict);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _managerMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingSaleReturnsAsExpected()
        {
            var sale = CreateSale();
            var expectedSale = new SaleDetailModel(sale);
            _managerMock.Setup(manager => manager.GetSpecificSale(It.IsAny<int>())).Returns(sale);
            var controller = new SaleController(_managerMock.Object);

            var response = controller.GetSale(sale.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedSale, okResponseObject.Value);
        }

        private Sale CreateSale()
        {
            return new Sale()
            {
                InvoiceLines = new List<InvoiceLine>()
            };
        }
    }
}
