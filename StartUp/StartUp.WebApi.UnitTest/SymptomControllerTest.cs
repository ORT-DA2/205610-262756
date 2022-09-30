using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.Domain;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.WebApi.UnitTest
{
    [TestClass]
    public class SymptomControllerTest
    {
        private Mock<ISymptomService> _managerMock;

        [TestInitialize]
        public void Setup()
        {
            _managerMock = new Mock<ISymptomService>(MockBehavior.Strict);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _managerMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingSymptomReturnsAsExpected()
        {
            var symptom = CreateSymptom();
            var expectedSymptom = new SymptomDetailModel(symptom);
            _managerMock.Setup(manager => manager.GetSpecificSymptom(It.IsAny<int>())).Returns(symptom);
            var controller = new SymptomController(_managerMock.Object);

            var response = controller.GetSymptom(symptom.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedSymptom, okResponseObject.Value);
        }

        private Symptom CreateSymptom()
        {
            return new Symptom()
            {
                SymptomDescription = "fiebre"
            };
        }
    }
}
