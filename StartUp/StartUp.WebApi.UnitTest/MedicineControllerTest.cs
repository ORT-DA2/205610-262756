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
    public class MedicineControllerTest
    {
        private Mock<IMedicineService> _serviceMock;
        private MedicineController _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IMedicineService>(MockBehavior.Strict);
            _controller = new MedicineController(_serviceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingMedicineReturnsAsExpected()
        {
            var medicine = CreateMedicine();
            var expectedMedicine = new MedicineDetailModel(medicine);
            _serviceMock.Setup(service => service.GetSpecificMedicine(It.IsAny<int>())).Returns(medicine);
            var controller = new MedicineController(_serviceMock.Object);

            var response = controller.GetMedicine(medicine.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedMedicine, okResponseObject.Value);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetNotExistingMedicineTest()
        {
            Medicine inv = CreateMedicine();
            _serviceMock.Setup(service => service.GetSpecificMedicine(It.IsAny<int>())).Returns((Medicine)null);

            _controller.GetMedicine(inv.Id);
        }
        
        [TestMethod]
        public void GetExistingMedicineWithModelTest()
        {
            MedicineSearchCriteriaModel medicineModel = new MedicineSearchCriteriaModel();
            List<Medicine> invList = new List<Medicine>();
            Medicine inv = CreateMedicine();
            invList.Add(inv);
            
            _serviceMock.Setup(service => service.GetAllMedicine(It.IsAny<MedicineSearchCriteria>())).Returns(invList);

            _controller.GetMedicine(medicineModel);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetNotExistingMedicineWithModelTest()
        {
            MedicineSearchCriteriaModel medicineModel = new MedicineSearchCriteriaModel();
            var exception = new ResourceNotFoundException("No medicines where found");

            _serviceMock.Setup(service => service.GetAllMedicine(It.IsAny<MedicineSearchCriteria>())).Throws(exception);

            var response = _controller.GetMedicine(medicineModel);
        }
        
        [TestMethod]
        public void CreateMedicineTest()
        {
            MedicineModel medicineModel = CreateMedicineModel();
            Medicine medicine = CreateMedicine();
            _serviceMock.Setup(server => server.CreateMedicine(It.IsAny<Medicine>())).Returns(medicine);

            var response = _controller.CreateMedicine(medicineModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.Created, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void UpdateMedicineTest()
        {
            MedicineModel medicineModel = CreateMedicineModel();
            Medicine medicine = CreateMedicine();
            medicineModel.Name = "Actron";
            _serviceMock.Setup(server => server.UpdateMedicine(medicine.Id, medicineModel.ToEntity())).Returns(medicineModel.ToEntity);

            var response = _controller.Update(medicine.Id, medicineModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void DeleteMedicineTest()
        {
            Medicine medicine = CreateMedicine();
            _serviceMock.Setup(server => server.DeleteMedicine(medicine.Id));

            var response = _controller.Delete(medicine.Id);
            var okResponseObject = response as OkResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
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
        
        private MedicineModel CreateMedicineModel()
        {
            return new MedicineModel()
            {
                Name = "Perifar",
                Prescription = false,
                Presentation = "20 comprimidos",
                Code = "ads456",
                Amount = 250,
                Measure = "para fiebre",
                Price = 300,
                Symptoms = new List<Symptom>()
            };
        }
    }
}
