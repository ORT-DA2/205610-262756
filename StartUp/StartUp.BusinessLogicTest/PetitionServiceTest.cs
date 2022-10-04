using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Moq;
using StartUp.BusinessLogic;
using StartUp.Domain;
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
        private PetitionService _service;
        private SessionService _sessionService;
        
        [TestInitialize]
        public void SetUp()
        {
            _repoMock = new Mock<IRepository<Petition>>(MockBehavior.Strict);
            _service = new PetitionService(_repoMock.Object, _pharmacyRepoMock.Object, _sessionService, _medicineRepoMock.Object);
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            _repoMock.VerifyAll();
        }
        
        [TestMethod]
        public void GetSpecificPetitionTest()
        {
            Petition petition = CreatePetition(1, 3, "Valium");
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Petition, bool>>>())).Returns(petition);

            var retrievedPetition = _service.GetSpecificPetition(petition.Id);
            
            Assert.AreEqual(petition.Id, retrievedPetition.Id);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetSpecificNullPetitionTest()
        {
            Petition petition = CreatePetition(1, 3, "Valium");
            
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Petition, bool>>>())).Returns((Petition)null);

            _service.GetSpecificPetition(petition.Id);
        }
        
        [TestMethod]
        public void GetAllPetitionsTest()
        {
            List<Petition> dummyPetitions = GenerateDummyPetition();
            _repoMock.Setup(repo => repo.GetAllByExpression(It.IsAny<Expression<Func<Petition, bool>>>())).Returns(dummyPetitions);
            PetitionSearchCriteria searchCriteria = new PetitionSearchCriteria();

            var retrievedPetitions = _service.GetAllPetition(searchCriteria);

            CollectionAssert.AreEqual(dummyPetitions, retrievedPetitions);
        }
        
        [TestMethod]
        public void UpdatePetitionTest()
        {
            Petition petition = CreatePetition(1, 3, "Valium");
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Petition, bool>>>())).Returns(petition);
            Petition updateData = CreatePetition(1, 3, "Valium");
            _repoMock.Setup(repo => repo.UpdateOne(petition));
            _repoMock.Setup(repo => repo.Save());
            
            Petition updatedPetition = _service.UpdatePetition(petition.Id, updateData);

            Assert.AreEqual(updatedPetition, petition);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteNotExistingPetitionTest()
        {
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Petition, bool>>>())).Returns((Petition)null);
            
            _service.DeletePetition(1);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeletePetitionTest()
        {
            Petition petition = CreatePetition(1, 3, "Valium");
            _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Petition, bool>>>())).Returns(petition).Returns((Petition)null);
            _repoMock.Setup(repo => repo.DeleteOne(petition));
            _repoMock.Setup(repo => repo.Save());
            
            _service.DeletePetition(petition.Id);

            _service.GetSpecificPetition(petition.Id);
        }

        [TestMethod]
        public void CreatePetitionTest()
        {
            Petition dummyPetition = CreatePetition(1,1,"valium");
            _repoMock.Setup(repo => repo.InsertOne(dummyPetition));
            _repoMock.Setup(repo => repo.Save());

            Petition newPetition = _service.CreatePetition(dummyPetition);

            Assert.AreEqual(newPetition, dummyPetition);
        }

        [TestMethod]
        [ExpectedException(typeof(InputException))]
        public void CreatePetitionWithAmount0Test()
        {
            Petition dummyPetition = CreatePetition(1,0,"OXA-B12");

            _service.CreatePetition(dummyPetition);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InputException))]
        public void CreatePetitionWithNullMedicineTest()
        {
            Petition dummyPetition = CreatePetition(1,3,null);

            _service.CreatePetition(dummyPetition);
        }
        
        private List<Petition> GenerateDummyPetition() => new List<Petition>()
        {
            new Petition() { Id = 2, Amount = 1, MedicineCode = "Perifae" },
            new Petition() { Id = 1, Amount = 3, MedicineCode = "Orudis" }
        };

        private Petition CreatePetition(int id, int amount, string med)
        {
            return new Petition()
            {
                Id = id,
                Amount = amount,
                MedicineCode = med
            };
        }
    }
}
