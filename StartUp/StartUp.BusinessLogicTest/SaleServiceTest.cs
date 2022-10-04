using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.BusinessLogic;
using StartUp.Domain;
using StartUp.Domain.Entities;
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
        private Mock<IDataAccess.IRepository<Pharmacy>> _repoPharmacyMock;
        private Mock<IDataAccess.IRepository<TokenAccess>> _repoTokenMock;
        private Mock<IDataAccess.IRepository<User>> _repoUserMock;
        private Mock<IDataAccess.IRepository<Session>> _repoSessionMock;
        private SessionService _sessionService;
        private SaleService _service;
        private List<InvoiceLine> invoiceLine;

        [TestInitialize]
        public void SetUp()
        {
            _repoMock = new Mock<IDataAccess.IRepository<Sale>>(MockBehavior.Strict);
            _repoPharmacyMock = new Mock<IDataAccess.IRepository<Pharmacy>>(MockBehavior.Strict);
            _repoUserMock = new Mock<IDataAccess.IRepository<User>>(MockBehavior.Strict);
            _repoTokenMock = new Mock<IDataAccess.IRepository<TokenAccess>>(MockBehavior.Strict);
            _repoSessionMock = new Mock<IDataAccess.IRepository<Session>>(MockBehavior.Strict);
            _sessionService = new SessionService(_repoSessionMock.Object, _repoUserMock.Object, _repoTokenMock.Object);
            _service = new SaleService(_repoMock.Object, _sessionService, _repoPharmacyMock.Object);
            invoiceLine = new List<InvoiceLine>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _repoMock.VerifyAll();
            _repoPharmacyMock.VerifyAll();
            _repoSessionMock.VerifyAll();
            _repoTokenMock.VerifyAll();
            _repoUserMock.VerifyAll();
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

            var retrievedSale = _service.GetAllSale();

            CollectionAssert.AreEqual(dummySale, retrievedSale);
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
