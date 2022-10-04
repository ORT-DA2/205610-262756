using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using StartUp.BusinessLogic;
using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.Domain;

namespace StartUp.BusinessLogicTest
{
    [TestClass]
    public class RequestServiceTest
    {
        
        private Mock<IDataAccess.IRepository<Request>> _repoMock;
        private Mock<IDataAccess.IRepository<Pharmacy>> _repoPharmacyMock;
        private Mock<IDataAccess.IRepository<TokenAccess>> _repoTokenMock;
        private Mock<IDataAccess.IRepository<User>> _repoUserMock;
        private Mock<IDataAccess.IRepository<Session>> _repoSessionMock;
        private RequestService _service;
        private SessionService _sessionService;
        private List<Petition> petitions;

        [TestInitialize]
        public void SetUp()
        {
            _repoMock = new Mock<IDataAccess.IRepository<Request>>(MockBehavior.Strict);
            _repoPharmacyMock = new Mock<IDataAccess.IRepository<Pharmacy>>(MockBehavior.Strict);
            _repoTokenMock = new Mock<IDataAccess.IRepository<TokenAccess>>(MockBehavior.Strict);
            _repoUserMock = new Mock<IDataAccess.IRepository<User>>(MockBehavior.Strict);
            _repoSessionMock = new Mock<IDataAccess.IRepository<Session>>(MockBehavior.Strict);
            _sessionService = new SessionService(_repoSessionMock.Object, _repoUserMock.Object, _repoTokenMock.Object);
            _service = new RequestService(_repoMock.Object, _sessionService, _repoPharmacyMock.Object);
            petitions = new List<Petition>();
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
        public void GetSpecificRequestTest()
        {
            var request = CreateRequest(1, petitions, true);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Request, bool>>>())).Returns(request);

            var retrievedRequest = _service.GetSpecificRequest(request.Id);
            
            Assert.AreEqual(request.Id, retrievedRequest.Id);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetSpecificNullRequestTest()
        {
            var request = CreateRequest(1, petitions, true);
            
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Request, bool>>>())).Returns((Request)null);

            _service.GetSpecificRequest(request.Id);
        }
        
        [TestMethod]
        public void GetAllRequestTest()
        {
            List<Request> dummyRequest = GenerateDummyRequest();
            _repoMock.Setup(repo => repo.GetAllByExpression(It.IsAny<Expression<Func<Request, bool>>>())).Returns(dummyRequest);
            RequestSearchCriteria searchCriteria = new RequestSearchCriteria();

            var retrievedRequest = _service.GetAllRequest(searchCriteria);

            CollectionAssert.AreEqual(dummyRequest, retrievedRequest);
        }
        
        [TestMethod]
        public void UpdateRequestTest()
        {
            var request = CreateRequest(1, petitions, true);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Request, bool>>>())).Returns(request);
            Request updateData = CreateRequest(request.Id, petitions, false);
            
            _repoMock.Setup(repo => repo.UpdateOne(request));
            _repoMock.Setup(repo => repo.Save());
            
            Request updatedRequest = _service.UpdateRequest(request.Id, updateData);

            Assert.AreEqual(updatedRequest, request);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteNotExistingRequestTest()
        {
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Request, bool>>>())).Returns((Request)null);
            
            _service.DeleteRequest(1);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteRequestTest()
        {
            var request = CreateRequest(1, petitions, true);
            _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Request, bool>>>())).Returns(request).Returns((Request)null);
            _repoMock.Setup(repo => repo.DeleteOne(request));
            _repoMock.Setup(repo => repo.Save());
            
            _service.DeleteRequest(request.Id);

            _service.GetSpecificRequest(request.Id);
        }
        
        [TestMethod]
        public void CreateRequestTest()
        {
            var request = CreateRequest(1, petitions, true);
            _repoMock.Setup(repo => repo.InsertOne(request));
            _repoMock.Setup(repo => repo.Save());

            Request newRequest = _service.CreateRequest(request);

            Assert.AreEqual(newRequest, request);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void CreateInvalidRequestTest()
        {
            var request = CreateRequest(1, null, true);
            
            _service.CreateRequest(request);
        }
        

        private List<Request> GenerateDummyRequest() => new List<Request>()
        {
            new Request() { Id= 1, Petitions = petitions},
            new Request() { Id= 2, Petitions = petitions}
        };
        
        private Request CreateRequest(int id, List<Petition> pet, bool state)
        {
            return new Request
            {
                Id = id,
                Petitions = pet,
            };
        }
    }
}
