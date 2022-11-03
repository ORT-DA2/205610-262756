using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using StartUp.BusinessLogic;
using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.Domain;
using StartUp.IDataAccess;

namespace StartUp.BusinessLogicTest
{
    [TestClass]
    public class RequestServiceTest
    {
        
        private Mock<IRepository<Request>> _repoMock;
        private Mock<IRepository<Pharmacy>> _pharmacyRepoMock;
        private Mock<IRepository<TokenAccess>> _tokenRepoMock;
        private Mock<IRepository<User>> _userRepoMock;
        private Mock<IRepository<Session>> _sessionRepoMock;
        private RequestService _service;
        private SessionService _sessionService;
        private List<Petition> _petitions;

        [TestInitialize]
        public void SetUp()
        {
            _repoMock = new Mock<IRepository<Request>>(MockBehavior.Strict);
            _sessionRepoMock = new Mock<IRepository<Session>>(MockBehavior.Strict);
            _userRepoMock = new Mock<IRepository<User>>(MockBehavior.Strict);
            _tokenRepoMock = new Mock<IRepository<TokenAccess>>(MockBehavior.Strict);
            _pharmacyRepoMock = new Mock<IRepository<Pharmacy>>(MockBehavior.Strict);
            //_sessionService = new SessionService(_sessionRepoMock.Object,_userRepoMock.Object,_tokenRepoMock.Object);
            _service = new RequestService(_repoMock.Object, _sessionService,_pharmacyRepoMock.Object );
            _petitions = new List<Petition>();
            SetSession();
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            _pharmacyRepoMock.VerifyAll();
            _tokenRepoMock.VerifyAll();
            _userRepoMock.VerifyAll();
            _sessionRepoMock.VerifyAll();
            _repoMock.VerifyAll();
        }
        
        [TestMethod]
        public void GetSpecificRequestTest()
        {
            var request = CreateRequest(1, _petitions, "Pending");
            _pharmacyRepoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(_sessionService.UserLogged.Pharmacy);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Request, bool>>>()))
                .Returns(request);
            
            var retrievedRequest = _service.GetSpecificRequest(request.Id);
            
            Assert.IsTrue(request.Id == retrievedRequest.Id);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InputException))]
        public void GetSpecificNotExistingRequestTest()
        {
            _pharmacyRepoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(_sessionService.UserLogged.Pharmacy);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Request, bool>>>())).Returns((Request)null);

            _service.GetSpecificRequest(7);
        }
        
        [TestMethod]
        public void GetAllRequestTest()
        {
            _pharmacyRepoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(_sessionService.UserLogged.Pharmacy);
            List<Request> list = new List<Request>();
            list.Concat(_sessionService.UserLogged.Pharmacy.Requests);
            RequestSearchCriteria searchCriteria = new RequestSearchCriteria();

            var retrievedRequest = _service.GetAllRequest(searchCriteria);

            CollectionAssert.AreEqual(list, retrievedRequest);
        }
        
        [TestMethod]
        public void UpdateRequestTest()
        {
            var request = CreateRequest(1, _petitions, "Pending");
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Request, bool>>>())).Returns(request);
            Request updateData = CreateRequest(request.Id, _petitions, "Pending");
            
            _repoMock.Setup(repo => repo.UpdateOne(request));
            _repoMock.Setup(repo => repo.Save());
            
            Request updatedRequest = _service.UpdateRequest(request.Id, updateData);

            Assert.AreEqual(updatedRequest, request);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InputException))]
        public void DeleteNotExistingRequestTest()
        {
            _pharmacyRepoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(_sessionService.UserLogged.Pharmacy);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Request, bool>>>())).Returns((Request)null);
            
            _service.DeleteRequest(7);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InputException))]
        public void DeleteRequestTest()
        {
            var request = CreateRequest(1, _petitions,"Pending");
            _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Request, bool>>>())).Returns(request).Returns((Request)null);
            _repoMock.Setup(repo => repo.DeleteOne(request));
            _repoMock.Setup(repo => repo.Save());
            
            _service.DeleteRequest(request.Id);

            _service.GetSpecificRequest(request.Id);
        }
        
        [TestMethod]
        public void CreateRequestTest()
        {
            var request = CreateRequest(1, _petitions, "Pending");
            _repoMock.Setup(repo => repo.InsertOne(request));
            _repoMock.Setup(repo => repo.Save());

            Request newRequest = _service.CreateRequest(request);

            Assert.AreEqual(newRequest, request);
        }
        
        [TestMethod]
        public void UpdateStockTest()
        {
            var request = CreateRequest(1, _petitions, "Pending");
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Request, bool>>>())).Returns(request);
            Request updateData = CreateRequest(request.Id, _petitions, "Approved");
            
            _repoMock.Setup(repo => repo.UpdateOne(request));
            _repoMock.Setup(repo => repo.Save());
            
            Request updatedRequest = _service.UpdateRequest(request.Id, updateData);

            Assert.AreEqual(updatedRequest, request);
        }

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
        
        private Request CreateRequest(int id, List<Petition> pet, string state)
        {
            Request req = new Request();
            req.Id = 1;
            req.Petitions = new List<Petition>();
            req.State = state;

            _pharmacyRepoMock.Setup(ph => 
                    ph.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(_sessionService.UserLogged.Pharmacy);
            _pharmacyRepoMock.Setup(ph => ph.UpdateOne(_sessionService.UserLogged.Pharmacy));
            _pharmacyRepoMock.Setup(ph => ph.Save());
            _repoMock.Setup(repo => repo.InsertOne(req));
            _repoMock.Setup(repo => repo.Save());
            _service.CreateRequest(req);
            _sessionService.UserLogged.Pharmacy.Requests.Add(req);
            
            return req;
        }
    }
}
