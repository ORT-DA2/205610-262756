using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Moq;
using StartUp.BusinessLogic;
using StartUp.Domain;
using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.IDataAccess;

namespace StartUp.BusinessLogicTest
{
    [TestClass]
    public class PetitionServiceTest
    {
        private Mock<IRepository<Petition>> _repoMock;
        private Mock<IRepository<Pharmacy>> _pharmacyRepoMock;
        private Mock<IRepository<Medicine>> _medicineRepoMock;
        private Mock<IRepository<Session>> _sessionRepoMock;
        private Mock<IRepository<User>> _userRepoMock;
        private Mock<IRepository<Request>> _requestRepoMock;
        private Mock<IRepository<TokenAccess>> _tokenRepoMock;
        private PetitionService _service;
        private SessionService _sessionService;
        private RequestService _requestService;
        
        [TestInitialize]
        public void SetUp()
        {
            _requestRepoMock = new Mock<IRepository<Request>>(MockBehavior.Strict);
            _pharmacyRepoMock = new Mock<IRepository<Pharmacy>>(MockBehavior.Strict);
            _medicineRepoMock = new Mock<IRepository<Medicine>>(MockBehavior.Strict);
            _repoMock = new Mock<IRepository<Petition>>(MockBehavior.Strict);
            _userRepoMock = new Mock<IRepository<User>>(MockBehavior.Strict);
            _sessionRepoMock = new Mock<IRepository<Session>>(MockBehavior.Strict);
            _tokenRepoMock = new Mock<IRepository<TokenAccess>>(MockBehavior.Strict);
            _sessionService = new SessionService(_sessionRepoMock.Object, _userRepoMock.Object, _tokenRepoMock.Object);
            _requestService = new RequestService(_requestRepoMock.Object, _sessionService, _pharmacyRepoMock.Object);
            _service = new PetitionService(_repoMock.Object, _pharmacyRepoMock.Object, _sessionService, _medicineRepoMock.Object);
            SetSession();
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            _medicineRepoMock.VerifyAll();
            _requestRepoMock.VerifyAll();
            _tokenRepoMock.VerifyAll();
            _userRepoMock.VerifyAll();
            _sessionRepoMock.VerifyAll();
            _pharmacyRepoMock.VerifyAll();
            _repoMock.VerifyAll();
        }
        
        [TestMethod]
        public void GetSpecificPetitionTest()
        {
            Petition petition = CreatePetition(1, 3, "ASW34");
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Petition, bool>>>()))
                .Returns(petition);
            
            var retrievedPetition = _service.GetSpecificPetition(petition.Id);
            
            Assert.IsTrue(petition.Id == retrievedPetition.Id);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetSpecificNotExistingPetitionTest()
        {
            Petition petition = new Petition();
            _pharmacyRepoMock.Setup(pRepo => pRepo
                    .GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(_sessionService.UserLogged.Pharmacy);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Petition, bool>>>())).Returns((Petition)null);

            _service.GetSpecificPetition(petition.Id);
        }
        
        [TestMethod]
        public void GetAllPetitionsTest()
        {
            _pharmacyRepoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(_sessionService.UserLogged.Pharmacy);
            PetitionSearchCriteria searchCriteria = new PetitionSearchCriteria();
            List<Petition> list = new List<Petition>();
            foreach (var pharmacyRequest in _sessionService.UserLogged.Pharmacy.Requests)
            {
                list.Concat(pharmacyRequest.Petitions);
            }
            
            var retrievedPetitions = _service.GetAllPetition(searchCriteria);

            CollectionAssert.AreEqual(list, retrievedPetitions);
        }
        
        [TestMethod]
        public void UpdatePetitionTest()
        {
            Petition petition = CreatePetition(1, 3, "ASW34");
            _pharmacyRepoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(_sessionService.UserLogged.Pharmacy)
                .Returns(_sessionService.UserLogged.Pharmacy);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Petition, bool>>>()))
                .Returns(petition);
            _medicineRepoMock.Setup(medRepo => medRepo.GetAllByExpression(It.IsAny<Expression<Func<Medicine, bool>>>()))
                .Returns(_sessionService.UserLogged.Pharmacy.Stock);
            Petition updateData = new Petition
            {
                Id = 1,
                MedicineCode = "XAR567",
                Amount = 60
            };
            _repoMock.Setup(repo => repo.UpdateOne(petition));
            _repoMock.Setup(repo => repo.Save());
            
            Petition updatedPetition = _service.UpdatePetition(petition.Id, updateData);

            Assert.AreEqual(updatedPetition, petition);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteNotExistingPetitionTest()
        {
            _pharmacyRepoMock.SetupSequence(phRepo =>
                    phRepo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(_sessionService.UserLogged.Pharmacy)
                .Returns(_sessionService.UserLogged.Pharmacy);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Petition, bool>>>()))
                .Returns((Petition)null);
            
            _service.DeletePetition(7);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InputException))]
        public void DeletePetitionTest()
        {
            Petition petition = CreatePetition(1, 3, "ASW34");
            _pharmacyRepoMock.SetupSequence(phRepo =>
                    phRepo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(_sessionService.UserLogged.Pharmacy)
                .Returns(_sessionService.UserLogged.Pharmacy);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Petition, bool>>>()))
                .Returns(petition);
            _pharmacyRepoMock.Setup(phRepo => phRepo.UpdateOne(_sessionService.UserLogged.Pharmacy));
            _pharmacyRepoMock.Setup(phRepo => phRepo.Save());
            _repoMock.Setup(repo => repo.DeleteOne(petition));
            _repoMock.Setup(repo => repo.Save());
            
            _service.DeletePetition(petition.Id);

            _service.GetSpecificPetition(petition.Id);
        }

        [TestMethod]
        public void CreatePetitionTest()
        {
            Petition dummyPetition = CreatePetition(1,1,"ASW34");
            _repoMock.Setup(repo => repo.InsertOne(dummyPetition));
            _repoMock.Setup(repo => repo.Save());

            Petition newPetition = _service.CreatePetition(dummyPetition);

            Assert.AreEqual(newPetition, dummyPetition);
        }

        [TestMethod]
        [ExpectedException(typeof(InputException))]
        public void CreatePetitionWithAmount0Test()
        {
            Petition dummyPetition = new Petition
            {
                Id = 2,
                MedicineCode = "ASW34",
                Amount = 0,
            };
            _pharmacyRepoMock.SetupSequence(pRepo => pRepo
                    .GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(_sessionService.UserLogged.Pharmacy);
            
            _service.CreatePetition(dummyPetition);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InputException))]
        public void CreatePetitionWithNullMedicineTest()
        {
            Petition dummyPetition = new Petition
            {
                Id = 2,
                MedicineCode = null,
                Amount = 0,
            };
            _pharmacyRepoMock.SetupSequence(pRepo => pRepo
                    .GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(_sessionService.UserLogged.Pharmacy);

            _service.CreatePetition(dummyPetition);
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
        
        private Request CreateRequest(Petition pet)
        {
            Request req = new Request();
            req.Id = 1;
            req.Petitions = new List<Petition>();
            req.Petitions.Add(pet);
            req.State = "Pending";
            
            _pharmacyRepoMock.Setup(pRepo => pRepo
                    .GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(_sessionService.UserLogged.Pharmacy);
            _pharmacyRepoMock.Setup(ph => ph.UpdateOne(_sessionService.UserLogged.Pharmacy));
            _pharmacyRepoMock.Setup(ph => ph.Save());
            _requestRepoMock.Setup(rRepo => rRepo.InsertOne(req));
            _requestRepoMock.Setup(rRepo => rRepo.Save());

            _requestService.CreateRequest(req);

            return req;
        }

        private Petition CreatePetition(int id, int amount, string med)
        {
           Petition petition = new Petition()
            {
                Id = id,
                Amount = amount,
                MedicineCode = med
            };
           
           _pharmacyRepoMock.Setup(pRepo => pRepo
                   .GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
               .Returns(_sessionService.UserLogged.Pharmacy);
           _repoMock.Setup(pet => pet.InsertOne(petition));
           _repoMock.Setup(pet => pet.Save());
           _medicineRepoMock.Setup(medR => medR
                   .GetAllByExpression(It.IsAny<Expression<Func<Medicine, bool>>>()))
               .Returns(_sessionService.UserLogged.Pharmacy.Stock.Where(m => m.Code == med));
           _service.CreatePetition(petition);
           _sessionService.UserLogged.Pharmacy.Requests.Add(CreateRequest(petition));
           

           return petition;
        }
    }
}
