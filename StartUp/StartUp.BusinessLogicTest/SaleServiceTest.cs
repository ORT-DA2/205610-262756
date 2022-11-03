using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.BusinessLogic;
using StartUp.Domain;
using StartUp.Domain.Entities;
using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using StartUp.IDataAccess;

namespace StartUp.BusinessLogicTest
{
    [TestClass]
    public class SaleServiceTest
    {
        private Mock<IDataAccess.IRepository<Sale>> _repoMock;
        private Mock<IDataAccess.IRepository<Pharmacy>> _pharmacyRepoMock;
        private Mock<IDataAccess.IRepository<TokenAccess>> _tokenRepoMock;
        private Mock<IDataAccess.IRepository<User>> _userRepoMock;
        private Mock<IDataAccess.IRepository<Session>> _sessionRepoMock;
        private SaleService _service;
        private SessionService _sessionService;
        private List<InvoiceLine> _invoiceLine;

        [TestInitialize]
        public void SetUp()
        {
            
            _repoMock = new Mock<IDataAccess.IRepository<Sale>>(MockBehavior.Strict);
            _pharmacyRepoMock = new Mock<IRepository<Pharmacy>>(MockBehavior.Strict);
            //_sessionService = new SessionService(_sessionRepoMock.Object,_userRepoMock.Object,_tokenRepoMock.Object);
            _service = new SaleService(_repoMock.Object, _sessionService, _pharmacyRepoMock.Object);
            _invoiceLine = new List<InvoiceLine>();
            SetSession();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _repoMock.VerifyAll();
            _sessionRepoMock.VerifyAll();
            _pharmacyRepoMock.VerifyAll();
            _tokenRepoMock.VerifyAll();
            _userRepoMock.VerifyAll();
        }

        [TestMethod]
        public void GetSpecificSaleTest()
        {
            var sale = CreateSale(1, _invoiceLine);
            _pharmacyRepoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(_sessionService.UserLogged.Pharmacy);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Sale, bool>>>())).Returns(sale);
            var retrievedSale = _service.GetSpecificSale(sale.Id);

            Assert.AreEqual(sale.Id, retrievedSale.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetSpecificNullSaleTest()
        {
            var sale = CreateSale(1, _invoiceLine);

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
            var sale = CreateSale(1, _invoiceLine);
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
            new Sale() { Id= 1, InvoiceLines = _invoiceLine},
            new Sale() { Id= 2, InvoiceLines = _invoiceLine}
        };

        private void SetSession()
        {
            Medicine medicine = new Medicine
            {
                Name = "clonazepam",
                Amount = 50,
                Code = "ASW34",
                Id = 1
            };
            Pharmacy pharmacy = new Pharmacy
            {
                Address = "hulahup",
                Name = "Machado",
                Sales = new List<Sale>(),
                Stock = new List<Medicine>(),
                Requests = new List<Request>()
            };
            pharmacy.Stock.Add(medicine);
            _sessionService.UserLogged = new User
            {
                Id = 1,
                Address = "justicia",
                Email = "something@gmail.com",
                Invitation = new Invitation(),
                Password = "12345678",
                RegisterDate = DateTime.Today,
                Pharmacy = pharmacy,
                Roles = new Role(),
                Token = new Guid().ToString()
            };
        }

        private InvoiceLine CreateInvoiceLine()
        {
            Medicine med = new Medicine
            {
                Amount = 6,
                Code = "QW567",
                Name = "Aspirina",
                Stock = 60
            };
            
            _sessionService.UserLogged.Pharmacy.Stock.Add(med);
            
            return new InvoiceLine
            {
                Amount = 3,
                Id = 1,
                Medicine = med
            };
        }

        private Sale CreateSale(int id, List<InvoiceLine> inv)
        {
            inv.Add(CreateInvoiceLine());
            
            Sale sale = new Sale
            {
                Id = id,
                InvoiceLines = inv
            };
            List<Pharmacy> pharmacies = new List<Pharmacy>();
            pharmacies.Add(_sessionService.UserLogged.Pharmacy);
            _pharmacyRepoMock.SetupSequence(pRepo => pRepo
                    .GetAllByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(pharmacies)
                .Returns(pharmacies);
            _pharmacyRepoMock.Setup(pRepo => pRepo
                .UpdateOne(_sessionService.UserLogged.Pharmacy));
            _pharmacyRepoMock.Setup(pRepo => pRepo.Save());
            _repoMock.Setup(repo => repo.InsertOne(sale));
            _repoMock.Setup(repo => repo.Save());
            _service.CreateSale(sale);
            _sessionService.UserLogged.Pharmacy.Sales.Add(sale);

            return sale;
        }
    }
}
