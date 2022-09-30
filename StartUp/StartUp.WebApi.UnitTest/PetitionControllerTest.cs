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
    public class PetitionControllerTest
    {
        private Mock<IPetitionService> _managerMock;

        [TestInitialize]
        public void Setup()
        {
            _managerMock = new Mock<IPetitionService>(MockBehavior.Strict);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _managerMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingPetitionReturnsAsExpected()
        {
            var petition = CreatePetition();
            var expectedPetition = new PetitionDetailModel(petition);
            _managerMock.Setup(manager => manager.GetSpecificPetition(It.IsAny<int>())).Returns(petition);
            var controller = new PetitionController(_managerMock.Object);

            var response = controller.GetPetition(petition.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedPetition, okResponseObject.Value);
        }

        private Petition CreatePetition()
        {
            return new Petition()
            {
               Amount = 45,
               MedicineCode = "sa46a66"
            };
        }
    }
}
