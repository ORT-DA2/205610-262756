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
    public class InvoiceLineControllerTest
    {
        private Mock<IInvoiceLineService> _serviceMock;
        private InvoiceLineController _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IInvoiceLineService>(MockBehavior.Strict);
            _controller = new InvoiceLineController(_serviceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingInvoiceLineReturnsAsExpected()
        {
            var invoiceLine = CreateInvoiceLine();
            var expectedInvoiceLine = new InvoiceLineDetailModel(invoiceLine);
            _serviceMock.Setup(manager => manager.GetSpecificInvoiceLine(It.IsAny<int>())).Returns(invoiceLine);
            var controller = new InvoiceLineController(_serviceMock.Object);

            var response = controller.GetInvoiceLine(invoiceLine.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedInvoiceLine, okResponseObject.Value);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetNotExistingInvoiceLineTest()
        {
            InvoiceLine inv = CreateInvoiceLine();
            _serviceMock.Setup(manager => manager.GetSpecificInvoiceLine(It.IsAny<int>())).Returns((InvoiceLine)null);

            _controller.GetInvoiceLine(inv.Id);
        }
        
        [TestMethod]
        public void GetExistingInvoiceLineWithModelTest()
        {
            InvoiceLineSearchCriteriaModel invoiceLineModel = new InvoiceLineSearchCriteriaModel();
            List<InvoiceLine> invList = new List<InvoiceLine>();
            InvoiceLine inv = CreateInvoiceLine();
            invList.Add(inv);
            
            _serviceMock.Setup(manager => manager.GetAllInvoiceLine(It.IsAny<InvoiceLineSearchCriteria>())).Returns(invList);

            _controller.GetInvoiceLine(invoiceLineModel);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetNotExistingInvoiceLineWithModelTest()
        {
            InvoiceLineSearchCriteriaModel invoiceLineModel = new InvoiceLineSearchCriteriaModel();
            var exception = new ResourceNotFoundException("No invoiceLines where found");

            _serviceMock.Setup(manager => manager.GetAllInvoiceLine(It.IsAny<InvoiceLineSearchCriteria>())).Throws(exception);

            var response = _controller.GetInvoiceLine(invoiceLineModel);
        }
        
        [TestMethod]
        public void CreateInvoiceLineTest()
        {
            InvoiceLineModel invoiceLineModel = CreateInvoiceLineModel();
            InvoiceLine invoiceLine = CreateInvoiceLine();
            _serviceMock.Setup(server => server.CreateInvoiceLine(It.IsAny<InvoiceLine>())).Returns(invoiceLine);

            var response = _controller.CreateInvoiceLine(invoiceLineModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.Created, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void UpdateInvoiceLineTest()
        {
            InvoiceLineModel invoiceLineModel = CreateInvoiceLineModel();
            InvoiceLine invoiceLine = CreateInvoiceLine();
            _serviceMock.Setup(server => server.UpdateInvoiceLine(invoiceLine.Id, invoiceLineModel.ToEntity())).Returns(invoiceLineModel.ToEntity);

            var response = _controller.Update(invoiceLine.Id, invoiceLineModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void DeleteInvoiceLineTest()
        {
            InvoiceLine invoiceLine = CreateInvoiceLine();
            _serviceMock.Setup(server => server.DeleteInvoiceLine(invoiceLine.Id));

            var response = _controller.Delete(invoiceLine.Id);
            var okResponseObject = response as OkResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }

        private InvoiceLine CreateInvoiceLine()
        {
            return new InvoiceLine()
            {
                Medicine = new Medicine(),
                Amount = 7
            };
        }
        
        private InvoiceLineModel CreateInvoiceLineModel()
        {
            Medicine medicine = new Medicine();
            return new InvoiceLineModel()
            {
                Amount = 7,
                Medicine = medicine
            };
        }
    }
}
