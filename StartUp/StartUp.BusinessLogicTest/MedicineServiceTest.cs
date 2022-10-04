using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
    public class MedicineServiceTest
    {
        
        private Mock<IRepository<Medicine>> _repoMock;
        private Mock<IRepository<Pharmacy>> _pharmacyRepoMock;
        private Mock<IRepository<Session>> _sessionRepoMock;
        private Mock<IRepository<User>> _userRepoMock;
        private Mock<IRepository<TokenAccess>> _tokenRepoMock;
        private SessionService _sessionService;
        private MedicineService _service;
        private List<Symptom> symptoms;
        
        [TestInitialize]
        public void SetUp()
        {
            _repoMock = new Mock<IRepository<Medicine>>(MockBehavior.Strict);
            _pharmacyRepoMock = new Mock<IRepository<Pharmacy>>(_pharmacyRepoMock.Object);
            _sessionRepoMock = new Mock<IRepository<Session>>(_sessionRepoMock.Object);
            _userRepoMock = new Mock<IRepository<User>>(_userRepoMock.Object);
            _tokenRepoMock = new Mock<IRepository<TokenAccess>>(_tokenRepoMock.Object);
            _sessionService = new SessionService(_sessionRepoMock.Object, _userRepoMock.Object, _tokenRepoMock.Object);
            _service = new MedicineService(_repoMock.Object, _sessionService, _pharmacyRepoMock.Object);
            symptoms = new List<Symptom>();
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            _repoMock.VerifyAll();
        }
        
        [TestMethod]
        public void GetSpecificMedicineTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                "Gotitas", 500, "ml", 1000, 15, true);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Medicine, bool>>>())).Returns(medicine);

            var retrievedMedicine = _service.GetSpecificMedicine(medicine.Id);
            
            Assert.AreEqual(medicine.Id, retrievedMedicine.Id);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetSpecificNullMedicineTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                "Gotitas", 500, "ml", 1000, 15, true);
            
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Medicine, bool>>>())).Returns((Medicine)null);

            _service.GetSpecificMedicine(medicine.Id);
        }
        
        [TestMethod]
        public void GetAllMedicinesTest()
        {
            List<Medicine> dummyMedicines = GenerateDummyMedicine();
            _repoMock.Setup(repo => repo.GetAllByExpression(It.IsAny<Expression<Func<Medicine, bool>>>())).Returns(dummyMedicines);
            MedicineSearchCriteria searchCriteria = new MedicineSearchCriteria();

            var retrievedMedicines = _service.GetAllMedicine(searchCriteria);

            CollectionAssert.AreEqual(dummyMedicines, retrievedMedicines);
        }
        
        [TestMethod]
        public void UpdateMedicineTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                "Gotitas", 500, "ml", 1000, 15, true);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Medicine, bool>>>())).Returns(medicine);
            Medicine updateData = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                "Pastillas", 500, "ml", 1000, 15, true);
            
            _repoMock.Setup(repo => repo.UpdateOne(medicine));
            _repoMock.Setup(repo => repo.Save());
            
            Medicine updatedMedicine = _service.UpdateMedicine(medicine.Id, updateData);

            Assert.AreEqual(updatedMedicine, medicine);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteNotExistingMedicineTest()
        {
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Medicine, bool>>>())).Returns((Medicine)null);
            
            _service.DeleteMedicine(1);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteMedicineTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                "Gotitas", 500, "ml", 1000, 15, true);
            _repoMock.SetupSequence(repo => 
                repo.GetOneByExpression(It.IsAny<Expression<Func<Medicine, bool>>>()))
                .Returns(medicine).Returns((Medicine)null);
            _repoMock.Setup(repo => repo.DeleteOne(medicine));
            _repoMock.Setup(repo => repo.Save());
            
            _service.DeleteMedicine(medicine.Id);

            _service.GetSpecificMedicine(medicine.Id);
        }

        [TestMethod]
        public void CreateMedicineTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                "Gotitas", 500, "ml", 1000, 15, true);
            _repoMock.Setup(repo => repo.InsertOne(medicine));
            _repoMock.Setup(repo => repo.Save());
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Medicine, bool>>>())).Returns((Medicine)null);

            Medicine newMedicine = _service.CreateMedicine(medicine);
            medicine.Code = newMedicine.Code;

            Assert.AreEqual(newMedicine, medicine);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InputException))]
        public void CreateExistingCodeMedicineTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                "Gotitas", 500, "ml", 1000, 15, true);
            _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Medicine, bool>>>()))
                .Returns(medicine);
            
            _service.CreateMedicine(medicine);
        }
        
        private List<Medicine> GenerateDummyMedicine() => new List<Medicine>()
        {
            new Medicine() { Id = 1, Code = "AQHLO4564", Name = "Clonapine", Symptoms = symptoms, 
                Presentation = "Gotitas", Amount = 500, Measure = "ml", Price = 1000, Stock = 15, Prescription = true },
            new Medicine() { Id = 2, Code = "AQHLO4567", Name = "Clonapine", Symptoms = symptoms, 
                Presentation = "Gotitas", Amount = 500, Measure = "ml", Price = 1000, Stock = 15, Prescription = true }
        };

        private Medicine CreateMedicine(int id, string code, string name, List<Symptom> lsintoms, string presentation, int amount, string measure, int price, int stock, bool prescription)
        {
            return new Medicine()
            {
                Id = id,
                Code = code,
                Name = name,
                Symptoms = lsintoms,
                Prescription = prescription,
                Presentation = presentation,
                Amount = amount,
                Measure = measure,
                Price = price,
                Stock = stock
            };
        }
    }
}
