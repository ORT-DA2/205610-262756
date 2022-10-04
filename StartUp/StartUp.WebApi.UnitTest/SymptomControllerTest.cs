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
    public class SymptomControllerTest
    {
        private Mock<ISymptomService> _serviceMock;
        private SymptomController _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<ISymptomService>(MockBehavior.Strict);
            _controller = new SymptomController(_serviceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingSymptomReturnsAsExpected()
        {
            var symptom = CreateSymptom();
            var expectedSymptom = new SymptomDetailModel(symptom);
            _serviceMock.Setup(service => service.GetSpecificSymptom(It.IsAny<int>())).Returns(symptom);
            var controller = new SymptomController(_serviceMock.Object);

            var response = controller.GetSymptom(symptom.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedSymptom, okResponseObject.Value);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetNotExistingSymptomTest()
        {
            Symptom inv = CreateSymptom();
            _serviceMock.Setup(service => service.GetSpecificSymptom(It.IsAny<int>())).Returns((Symptom)null);

            _controller.GetSymptom(inv.Id);
        }
        
        [TestMethod]
        public void GetExistingSymptomWithModelTest()
        {
            SymptomSearchCriteriaModel symptomModel = new SymptomSearchCriteriaModel();
            List<Symptom> invList = new List<Symptom>();
            Symptom inv = CreateSymptom();
            invList.Add(inv);
            
            _serviceMock.Setup(service => service.GetAllSymptom(It.IsAny<SymptomSearchCriteria>())).Returns(invList);

            _controller.GetSymptom(symptomModel);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetNotExistingSymptomWithModelTest()
        {
            SymptomSearchCriteriaModel symptomModel = new SymptomSearchCriteriaModel();
            var exception = new ResourceNotFoundException("No symptoms where found");

            _serviceMock.Setup(service => service.GetAllSymptom(It.IsAny<SymptomSearchCriteria>())).Throws(exception);

            var response = _controller.GetSymptom(symptomModel);
        }
        
        [TestMethod]
        public void CreateSymptomTest()
        {
            SymptomModel symptomModel = CreateSymptomModel();
            Symptom symptom = CreateSymptom();
            _serviceMock.Setup(server => server.CreateSymptom(It.IsAny<Symptom>())).Returns(symptom);

            var response = _controller.CreateSymptom(symptomModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.Created, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void UpdateSymptomTest()
        {
            SymptomModel symptomModel = CreateSymptomModel();
            Symptom symptom = CreateSymptom();
            symptomModel.SymptomDescription = "feber";
            _serviceMock.Setup(server => server.UpdateSymptom(symptom.Id, symptomModel.ToEntity())).Returns(symptomModel.ToEntity);

            var response = _controller.Update(symptom.Id, symptomModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void DeleteSymptomTest()
        {
            Symptom symptom = CreateSymptom();
            _serviceMock.Setup(server => server.DeleteSymptom(symptom.Id));

            var response = _controller.DeleteSymptom(symptom.Id);
            var okResponseObject = response as OkResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }

        private Symptom CreateSymptom()
        {
            return new Symptom()
            {
                SymptomDescription = "fiebre"
            };
        }
        
        private SymptomModel CreateSymptomModel()
        {
            return new SymptomModel()
            {
                SymptomDescription = "Depression"
            };
        }
    }
}
