using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.BusinessLogic;
using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace StartUp.BusinessLogicTest
{
    [TestClass]
    public class SaleServiceTest
    {
        private Mock<IDataAccess.IRepository<Sale>> _repoMock;
        private SaleService _service;
        private List<InvoiceLine> invoiceLine;

        [TestInitialize]
        public void SetUp()
        {
            _repoMock = new Mock<IDataAccess.IRepository<Sale>>(MockBehavior.Strict);
            _service = new SaleService(_repoMock.Object);
            invoiceLine = new List<InvoiceLine>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _repoMock.VerifyAll();
        }

        [TestMethod]
        public void GetSpecificSaleTest()
        {
            var sale = CreateSale(1, invoiceLine);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Sale, bool>>>())).Returns(sale);

            var retrievedSale = _service.GetSpecificSale(sale.Id);

            Assert.AreEqual(sale.Id, retrievedSale.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetSpecificNullSaleTest()
        {
            var sale = CreateSale(1, invoiceLine);

            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Sale, bool>>>())).Returns((Sale)null);

            _service.GetSpecificSale(sale.Id);
        }

        [TestMethod]
        public void GetAllSaleTest()
        {
            List<Sale> dummySale = GenerateDummySale();
            _repoMock.Setup(repo => repo.GetAllByExpression(It.IsAny<Expression<Func<Sale, bool>>>())).Returns(dummySale);
            SaleSearchCriteria searchCriteria = new SaleSearchCriteria();

            var retrievedSale = _service.GetAllSale(searchCriteria);

            CollectionAssert.AreEqual(dummySale, retrievedSale);
        }

        [TestMethod]
        public void UpdateSaleTest()
        {
            var sale = CreateSale(1, invoiceLine);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Sale, bool>>>())).Returns(sale);
            Sale updateData = CreateSale(sale.Id, invoiceLine);

            _repoMock.Setup(repo => repo.UpdateOne(sale));
            _repoMock.Setup(repo => repo.Save());

            Sale updatedSale = _service.UpdateSale(sale.Id, updateData);

            Assert.AreEqual(updatedSale, sale);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteNotExistingSaleTest()
        {
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Sale, bool>>>())).Returns((Sale)null);

            _service.DeleteSale(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteSaleTest()
        {
            var sale = CreateSale(1, invoiceLine);
            _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Sale, bool>>>())).Returns(sale).Returns((Sale)null);
            _repoMock.Setup(repo => repo.DeleteOne(sale));
            _repoMock.Setup(repo => repo.Save());

            _service.DeleteSale(sale.Id);

            _service.GetSpecificSale(sale.Id);
        }

        [TestMethod]
        public void CreateSaleTest()
        {
            var sale = CreateSale(1, invoiceLine);
            _repoMock.Setup(repo => repo.InsertOne(sale));
            _repoMock.Setup(repo => repo.Save());

            Sale newSale = _service.CreateSale(sale);

            Assert.AreEqual(newSale, sale);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void CreateInvalidSaleTest()
        {
            var sale = CreateSale(1, null);

            _service.CreateSale(sale);
        }


        private List<Sale> GenerateDummySale() => new List<Sale>()
        {
            new Sale() { Id= 1, InvoiceLines = invoiceLine},
            new Sale() { Id= 2, InvoiceLines = invoiceLine}
        };

        private Sale CreateSale(int id, List<InvoiceLine> inv)
        {
            return new Sale
            {
                Id = id,
                InvoiceLines = inv
            };
        }
    }
}
