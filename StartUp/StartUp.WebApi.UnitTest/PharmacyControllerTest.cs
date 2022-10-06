using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.Models.Models.In;

namespace StartUp.WebApi.UnitTest
{
    [TestClass]
    public class PharmacyControllerTest
    {
        private Mock<IPharmacyService> _serviceMock;
        private PharmacyController _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IPharmacyService>(MockBehavior.Strict);
            _controller = new PharmacyController(_serviceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingPharmacyTest()
        {
            var pharmacy = CreatePharmacy();
            var expectedPharmacy = new PharmacyDetailModel(pharmacy);
            _serviceMock.Setup(manager => manager.GetSpecificPharmacy(It.IsAny<int>())).Returns(pharmacy);

            var response = _controller.GetPharmacy(pharmacy.Id);
            var okResponseObject = response as OkObjectResult;

            Assert.AreEqual(expectedPharmacy, okResponseObject.Value);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetNotExistingPharmacyTest()
        {
            Pharmacy pharm = CreatePharmacy();
            _serviceMock.Setup(manager => manager.GetSpecificPharmacy(It.IsAny<int>())).Returns((Pharmacy)null);

            _controller.GetPharmacy(pharm.Id);
        }
        
        [TestMethod]
        public void GetExistingPharmacyWithModelTest()
        {
            PharmacySearchCriteriaModel pharmacyModel = new PharmacySearchCriteriaModel();
            pharmacyModel.Name = "El tunel";
            pharmacyModel.Address = "Solano Garcia";
            List<Pharmacy> pharmList = new List<Pharmacy>();
            Pharmacy pharm = CreatePharmacy();
            pharmList.Add(pharm);
            
            _serviceMock.Setup(manager => manager.GetAllPharmacy(It.IsAny<PharmacySearchCriteria>())).Returns(pharmList);

            _controller.GetPharmacy(pharmacyModel);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetNotExistingPharmacyWithModelTest()
        {
            PharmacySearchCriteriaModel pharmacyModel = new PharmacySearchCriteriaModel();
            var exception = new ResourceNotFoundException("No pharmacys where found");

            _serviceMock.Setup(manager => manager.GetAllPharmacy(It.IsAny<PharmacySearchCriteria>())).Throws(exception);

            var response = _controller.GetPharmacy(pharmacyModel);
        }
        
        [TestMethod]
        public void CreatePharmacyTest()
        {
            PharmacyModel pharmacyModel = CreatePharmacyModel();
            Pharmacy pharmacy = CreatePharmacy();
            _serviceMock.Setup(server => server.CreatePharmacy(It.IsAny<Pharmacy>())).Returns(pharmacy);

            var response = _controller.CreatePharmacy(pharmacyModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.Created, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void UpdatePharmacyTest()
        {
            PharmacyModel pharmacyModel = CreatePharmacyModel();
            Pharmacy pharmacy = CreatePharmacy();
            pharmacyModel.Name = "El tunel";
            _serviceMock.Setup(server => server.UpdatePharmacy(pharmacy.Id, pharmacyModel.ToEntity())).Returns(pharmacyModel.ToEntity);

            var response = _controller.UpdatePharmacy(pharmacy.Id, pharmacyModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void DeletePharmacyTest()
        {
            Pharmacy pharmacy = CreatePharmacy();
            _serviceMock.Setup(server => server.DeletePharmacy(pharmacy.Id));

            var response = _controller.DeletePharmacy(pharmacy.Id);
            var okResponseObject = response as OkResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }

        private Pharmacy CreatePharmacy()
        {
            return new Pharmacy()
            {
                Id = 1,
                Name = "Pharmashop",
                Stock = new List<Medicine>(),
                Requests = new List<Request>(),
                Address = "Guayaqui",
                Sales = new List<Sale>()
            };
        }
        
        private PharmacyModel CreatePharmacyModel()
        {
            return new PharmacyModel()
            {
                Name = "Pharmashop",
                Stock = new List<Medicine>(),
                Requests = new List<Request>(),
                Address = "Guayaqui",
            };
        }
    }
}