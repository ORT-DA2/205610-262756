using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.Domain;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Controllers;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using StartUp.Models.Models.In;

namespace StartUp.WebApi.UnitTest
{
    [TestClass]
    public class SaleControllerTest
    {
        private Mock<ISaleService> _serviceMock;
        private SaleController _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<ISaleService>(MockBehavior.Strict);
            _controller = new SaleController(_serviceMock.Object);

        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingSaleReturnsAsExpected()
        {
            var sale = CreateSale();
            var expectedSale = new SaleDetailModel(sale);
            _serviceMock.Setup(service => service.GetSpecificSale(It.IsAny<int>())).Returns(sale);
            var controller = new SaleController(_serviceMock.Object);

            var response = controller.GetSale(sale.Id);
            var okResponseObject = response as OkObjectResult;

            Assert.AreEqual(expectedSale, okResponseObject.Value);
        }

        [TestMethod]
        public void GetAllSaleTest()
        {
            List<Sale> saleList = new List<Sale>();
            Sale inv = CreateSale();
            saleList.Add(inv);
            
            _serviceMock.Setup(manager => manager.GetAllSale()).Returns(saleList);

            var response = _controller.GetSale();
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
            
        }

        [TestMethod]
        public void CreateSaleTest()
        {
            SaleModel saleModel = CreateSaleModel();
            Sale sale = CreateSale();
            _serviceMock.Setup(server => server.CreateSale(It.IsAny<Sale>())).Returns(sale);

            var response = _controller.CreateSale(saleModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.Created, okResponseObject.StatusCode);
        }
        
        private Sale CreateSale()
        {
            return new Sale()
            {
                InvoiceLines = new List<InvoiceLine>()
            };
        }
        
        private SaleModel CreateSaleModel()
        {
            return new SaleModel()
            {
                InvoiceLines = new List<InvoiceLine>()
            };
        }
    }
}
