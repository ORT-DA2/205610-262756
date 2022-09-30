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
    public class PharmacyServiceTest
    {
        private Mock<IRepository<Pharmacy>> _repoMock;
        private PharmacyService _service;
        private List<Medicine> listMedicines;
        private List<Request> listRequests;
        
        [TestInitialize]
        public void SetUp()
        {
            _repoMock = new Mock<IRepository<Pharmacy>>(MockBehavior.Strict);
            _service = new PharmacyService(_repoMock.Object);
            listMedicines = new List<Medicine>();
            listRequests = new List<Request>();
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            _repoMock.VerifyAll();
        }
        
        [TestMethod]
        public void GetSpecificPharmacyTest()
        {
            var pharmacy = CreatePharmacy("El tunel", "18 de julio 1189", listMedicines, listRequests);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>())).Returns(pharmacy);

            var retrievedPharmacy = _service.GetSpecificPharmacy(pharmacy.Id);
            
            Assert.AreEqual(pharmacy.Id, retrievedPharmacy.Id);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetSpecificNullPharmacyTest()
        {
            var pharmacy = CreatePharmacy("El tunel", "18 de julio 1189", listMedicines, listRequests);
            
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>())).Returns((Pharmacy)null);

            _service.GetSpecificPharmacy(pharmacy.Id);
        }
        
        [TestMethod]
        public void GetAllPharmacysTest()
        {
            List<Pharmacy> dummyPharmacy = GenerateDummyPharmacy();
            _repoMock.Setup(repo => repo.GetAllByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>())).Returns(dummyPharmacy);
            PharmacySearchCriteria searchCriteria = new PharmacySearchCriteria();

            var retrievedPharmacys = _service.GetAllPharmacy(searchCriteria);

            CollectionAssert.AreEqual(dummyPharmacy, retrievedPharmacys);
        }
        
        [TestMethod]
        public void UpdatePharmacyTest()
        {
            var pharmacy = CreatePharmacy("El tunel", "18 de julio 1189", listMedicines, listRequests);
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>())).Returns(pharmacy);
            Pharmacy updateData = new Pharmacy()
            {
                Id = pharmacy.Id,
                Name = "ElTunelito",
                Address = "Justicia",
                Stock = new List<Medicine>(),
                Requests = new List<Request>()
            };
            _repoMock.Setup(repo => repo.UpdateOne(pharmacy));
            _repoMock.Setup(repo => repo.Save());
            
            Pharmacy updatedPharmacy = _service.UpdatePharmacy(pharmacy.Id, updateData);

            Assert.AreEqual(updatedPharmacy, pharmacy);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteNotExistingPharmacyTest()
        {
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>())).Returns((Pharmacy)null);
            
            _service.DeletePharmacy(1);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeletePharmacyTest()
        {
            var pharmacy = CreatePharmacy("El tunel", "18 de julio 1189", listMedicines, listRequests);
            _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>())).Returns(pharmacy).Returns((Pharmacy)null);
            _repoMock.Setup(repo => repo.DeleteOne(pharmacy));
            _repoMock.Setup(repo => repo.Save());
            
            _service.DeletePharmacy(pharmacy.Id);

            _service.GetSpecificPharmacy(pharmacy.Id);
        }
        
        [TestMethod]
        public void CreatePharmacyTest()
        {
            var pharmacy = CreatePharmacy("El tunel", "18 de julio 1189", listMedicines, listRequests);
            _repoMock.Setup(repo => repo.InsertOne(pharmacy));
            _repoMock.Setup(repo => repo.Save());
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>())).Returns((Pharmacy)null);

            Pharmacy newPharmacy = _service.CreatePharmacy(pharmacy);

            Assert.AreEqual(newPharmacy, pharmacy);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void CreateExistingPharmacyTest()
        {
            var pharmacy = CreatePharmacy("El tunel", "18 de julio 1189", listMedicines, listRequests);
            _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(pharmacy);
            
            _service.CreatePharmacy(pharmacy);
        }
        
        private List<Pharmacy> GenerateDummyPharmacy() => new List<Pharmacy>()
        {
            new Pharmacy() { Name = "El tunel", Address = "Chucarro", Requests = listRequests, Stock = listMedicines},
            new Pharmacy() { Name = "Farmashop", Address = "Coronel Mora", Requests = listRequests, Stock = listMedicines}
        };
        
        private Pharmacy CreatePharmacy(string pharmacyName, string address, List<Medicine> lMedicine, List<Request> lRequest)
        {
            return new Pharmacy
            {
                Name = pharmacyName, 
                Address = address,
                Stock = lMedicine,
                Requests = lRequest
            };
        }
    }
}
