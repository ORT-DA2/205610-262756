using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.Domain;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
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
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedSale, okResponseObject.Value);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetNotExistingSaleTest()
        {
            Sale inv = CreateSale();
            _serviceMock.Setup(manager => manager.GetSpecificSale(It.IsAny<int>())).Returns((Sale)null);

            _controller.GetSale(inv.Id);
        }
        
        [TestMethod]
        public void GetExistingSaleWithModelTest()
        {
            SaleSearchCriteriaModel saleModel = new SaleSearchCriteriaModel();
            List<Sale> invList = new List<Sale>();
            Sale inv = CreateSale();
            invList.Add(inv);
            
            _serviceMock.Setup(manager => manager.GetAllSale(It.IsAny<SaleSearchCriteria>())).Returns(invList);

            _controller.GetSale(saleModel);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetNotExistingSaleWithModelTest()
        {
            SaleSearchCriteriaModel saleModel = new SaleSearchCriteriaModel();
            var exception = new ResourceNotFoundException("No sales where found");

            _serviceMock.Setup(manager => manager.GetAllSale(It.IsAny<SaleSearchCriteria>())).Throws(exception);

            var response = _controller.GetSale(saleModel);
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
        
        [TestMethod]
        public void UpdateSaleTest()
        {
            SaleModel saleModel = CreateSaleModel();
            Sale sale = CreateSale();
            _serviceMock.Setup(server => server.UpdateSale(sale.Id, saleModel.ToEntity())).Returns(saleModel.ToEntity);

            var response = _controller.Update(sale.Id, saleModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void DeleteSaleTest()
        {
            Sale sale = CreateSale();
            _serviceMock.Setup(server => server.DeleteSale(sale.Id));

            var response = _controller.Delete(sale.Id);
            var okResponseObject = response as OkResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
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
