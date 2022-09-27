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
    public class MedicineControllerTest
    {
        private Mock<IMedicineManager> _managerMock;

        [TestInitialize]
        public void Setup()
        {
            _managerMock = new Mock<IMedicineManager>(MockBehavior.Strict);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _managerMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingMedicineReturnsAsExpected()
        {
            var medicine = CreateMedicine();
            var expectedMedicine = new MedicineDetailModel(medicine);
            _managerMock.Setup(manager => manager.GetSpecificMedicine(It.IsAny<int>())).Returns(medicine);
            var controller = new MedicineController(_managerMock.Object);

            var response = controller.GetMedicine(medicine.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedMedicine, okResponseObject.Value);
        }

        private Medicine CreateMedicine()
        {
            return new Medicine()
            {
                Name = "Perifar",
                Prescription = false,
                Presentation = "20 comprimidos",
                Code = "ads456",
                Amount = 250,
                Measure = "para fiebre",
                Price = 300,
                Stock = 100,
                Symptoms = new List<Symptom>()

            };
        }
    }
}
