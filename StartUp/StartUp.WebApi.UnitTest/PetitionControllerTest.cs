using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.Domain;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.Models.Models.In;

namespace StartUp.WebApi.UnitTest
{
    [TestClass]
    public class PetitionControllerTest
    {
        private Mock<IPetitionService> _serviceMock;
        private PetitionController _controller;


        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IPetitionService>(MockBehavior.Strict);
            _controller = new PetitionController(_serviceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingPetitionReturnsAsExpected()
        {
            var petition = CreatePetition();
            var expectedPetition = new PetitionDetailModel(petition);
            _serviceMock.Setup(service => service.GetSpecificPetition(It.IsAny<int>())).Returns(petition);
            var controller = new PetitionController(_serviceMock.Object);

            var response = controller.GetPetition(petition.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedPetition, okResponseObject.Value);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetNotExistingPetitionTest()
        {
            Petition inv = CreatePetition();
            _serviceMock.Setup(service => service.GetSpecificPetition(It.IsAny<int>())).Returns((Petition)null);

            _controller.GetPetition(inv.Id);
        }
        
        [TestMethod]
        public void GetExistingPetitionWithModelTest()
        {
            PetitionSearchCriteriaModel petitionModel = new PetitionSearchCriteriaModel();
         
            List<Petition> invList = new List<Petition>();
            Petition inv = CreatePetition();
            invList.Add(inv);
            
            _serviceMock.Setup(service => service.GetAllPetition(It.IsAny<PetitionSearchCriteria>())).Returns(invList);

            _controller.GetPetition(petitionModel);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetNotExistingPetitionWithModelTest()
        {
            PetitionSearchCriteriaModel petitionModel = new PetitionSearchCriteriaModel();
            var exception = new ResourceNotFoundException("No petitions where found");

            _serviceMock.Setup(service => service.GetAllPetition(It.IsAny<PetitionSearchCriteria>())).Throws(exception);

            var response = _controller.GetPetition(petitionModel);
        }
        
        [TestMethod]
        public void CreatePetitionTest()
        {
            PetitionModel petitionModel = CreatePetitionModel();
            Petition petition = CreatePetition();
            _serviceMock.Setup(server => server.CreatePetition(It.IsAny<Petition>())).Returns(petition);

            var response = _controller.CreatePetition(petitionModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.Created, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void UpdatePetitionTest()
        {
            PetitionModel petitionModel = CreatePetitionModel();
            Petition petition = CreatePetition();
            petitionModel.Amount = 100;
            _serviceMock.Setup(server => server.UpdatePetition(petition.Id, petitionModel.ToEntity())).Returns(petitionModel.ToEntity);

            var response = _controller.Update(petition.Id, petitionModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void DeletePetitionTest()
        {
            Petition petition = CreatePetition();
            _serviceMock.Setup(server => server.DeletePetition(petition.Id));

            var response = _controller.Delete(petition.Id);
            var okResponseObject = response as OkResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }

        private Petition CreatePetition()
        {
            return new Petition()
            {
               Amount = 45,
               Id = 1,
               MedicineCode = "Ax567UI"
            };
        }
        
        private PetitionModel CreatePetitionModel()
        {
            return new PetitionModel()
            {
                Amount = 45,
                MedicineCode = "Ax567UI"
            };
        }
    }
}
