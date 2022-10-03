using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using StartUp.BusinessLogic;
using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.IDataAccess;

namespace StartUp.BusinessLogicTest
{
    [TestClass]
    public class InvoiceLineServiceTest
    {
        private Mock<IRepository<InvoiceLine>> _repoMock;
        private InvoiceLineService _service;
        private Medicine medicine;
            
        [TestInitialize]
        public void SetUp()
        {
            _repoMock = new Mock<IRepository<InvoiceLine>>(MockBehavior.Strict);
            _service = new InvoiceLineService(_repoMock.Object);
            medicine = new Medicine();
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            _repoMock.VerifyAll();
        }
        
        [TestMethod]
        public void GetSpecificInvoiceLineTest()
        {
            var invoiceLine = CreateInvoiceLine(1,1,medicine);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<InvoiceLine, bool>>>())).Returns(invoiceLine);

            var retrievedInvoiceLine = _service.GetSpecificInvoiceLine(invoiceLine.Id);
            
            Assert.AreEqual(invoiceLine.Id, retrievedInvoiceLine.Id);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetSpecificNullInvoiceLineTest()
        {
            var invoiceLine = CreateInvoiceLine(1,1,medicine);
            
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<InvoiceLine, bool>>>())).Returns((InvoiceLine)null);

            _service.GetSpecificInvoiceLine(invoiceLine.Id);
        }
        
        [TestMethod]
        public void GetAllInvoiceLinesTest()
        {
            List<InvoiceLine> dummyInvoiceLines = GenerateDummyInvoiceLine();
            _repoMock.Setup(repo => repo.GetAllByExpression(It.IsAny<Expression<Func<InvoiceLine, bool>>>())).Returns(dummyInvoiceLines);
            InvoiceLineSearchCriteria searchCriteria = new InvoiceLineSearchCriteria();

            var retrievedInvoiceLines = _service.GetAllInvoiceLine(searchCriteria);

            CollectionAssert.AreEqual(dummyInvoiceLines, retrievedInvoiceLines);
        }
        
        [TestMethod]
        public void UpdateInvoiceLineTest()
        {
            var invoiceLine = CreateInvoiceLine(1,1,medicine);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<InvoiceLine, bool>>>())).Returns(invoiceLine);
            InvoiceLine updateData = CreateInvoiceLine(1, 3, medicine);
            _repoMock.Setup(repo => repo.UpdateOne(invoiceLine));
            _repoMock.Setup(repo => repo.Save());
            
            InvoiceLine updatedInvoiceLine = _service.UpdateInvoiceLine(invoiceLine.Id, updateData);

            Assert.AreEqual(updatedInvoiceLine, invoiceLine);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteNotExistingInvoiceLineTest()
        {
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<InvoiceLine, bool>>>())).Returns((InvoiceLine)null);
            
            _service.DeleteInvoiceLine(1);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteInvoiceLineTest()
        {
            var invoiceLine = CreateInvoiceLine(1,1,medicine);
            _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<InvoiceLine, bool>>>())).Returns(invoiceLine).Returns((InvoiceLine)null);
            _repoMock.Setup(repo => repo.DeleteOne(invoiceLine));
            _repoMock.Setup(repo => repo.Save());
            
            _service.DeleteInvoiceLine(invoiceLine.Id);

            _service.GetSpecificInvoiceLine(invoiceLine.Id);
        }

        [TestMethod]
        public void CreateInvoiceLineTest()
        {
            InvoiceLine dummyInvoiceLine = CreateInvoiceLine(1,1,medicine);
            _repoMock.Setup(repo => repo.InsertOne(dummyInvoiceLine));
            _repoMock.Setup(repo => repo.Save());

            InvoiceLine newInvoiceLine = _service.CreateInvoiceLine(dummyInvoiceLine);

            Assert.AreEqual(newInvoiceLine, dummyInvoiceLine);
        }

        [TestMethod]
        [ExpectedException(typeof(InputException))]
        public void CreateInvoiceLineWithAmount0Test()
        {
            InvoiceLine dummyInvoiceLine = CreateInvoiceLine(1,0,medicine);

            _service.CreateInvoiceLine(dummyInvoiceLine);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InputException))]
        public void CreateInvoiceLineWithNullMedicineTest()
        {
            InvoiceLine dummyInvoiceLine = CreateInvoiceLine(1,3,null);

            _service.CreateInvoiceLine(dummyInvoiceLine);
        }
        
        private List<InvoiceLine> GenerateDummyInvoiceLine() => new List<InvoiceLine>()
        {
            new InvoiceLine() { Id = 2, Amount = 1, Medicine = medicine },
            new InvoiceLine() { Id = 1, Amount = 3, Medicine = medicine }
        };

        private InvoiceLine CreateInvoiceLine(int id, int amount, Medicine medicine)
        {
            return new InvoiceLine()
            {
                Id = 1,
                Amount = amount,
                Medicine = medicine
            };
        }
    }
}
