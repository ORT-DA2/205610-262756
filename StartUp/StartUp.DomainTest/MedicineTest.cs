using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.DomainTest
{
    [TestClass]
    public class MedicineTest
    {
        List<Symptom> symptoms;

        [TestInitialize]
        public void Setup()
        {
           symptoms = new List<Symptom>();
        }

        [TestMethod]
        public void NewMedicineTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                "Gotitas", 500, "ml", 1000, 15, true);

            medicine.IsValidMedicine();
            
            Assert.IsNotNull(medicine);
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewMedicineWithNoCodeTest()
        {
            Medicine medicine = CreateMedicine(1, "", "Clonapine", symptoms, 
                "Gotitas", 500, "ml", 1000, 15, true);

            medicine.IsValidMedicine();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewMedicineWithSpacesCodeTest()
        {
            Medicine medicine = CreateMedicine(1, "   ", "Clonapine", symptoms, 
                "Gotitas", 500, "ml", 1000, 15, true);

            medicine.IsValidMedicine();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewMedicineWithNoNameTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "", symptoms, 
                "Gotitas", 500, "ml", 1000, 15, true);

            medicine.IsValidMedicine();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewMedicineWithSpacesNameTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "    ", symptoms, 
                "Gotitas", 500, "ml", 1000, 15, true);

            medicine.IsValidMedicine();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewMedicineWithNoSymptomsTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", null, 
                "Gotitas", 500, "ml", 1000, 15, true);

            medicine.IsValidMedicine();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewMedicineWithNoPresentationTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                "", 500, "ml", 1000, 15, true);

            medicine.IsValidMedicine();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewMedicineWithSpacesPresentationTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                "    ", 500, "ml", 1000, 15, true);

            medicine.IsValidMedicine();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewMedicineWith0AmountPresentationTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                "Drops", 0, "ml", 1000, 15, true);

            medicine.IsValidMedicine();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewMedicineWithNoMessureTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                "Drops", 1, "", 1000, 15, true);

            medicine.IsValidMedicine();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewMedicineWithOnlySpacesMessureTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                "Drops", 1, "   ", 1000, 15, true);

            medicine.IsValidMedicine();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewMedicineWithPrice0Test()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                "Drops", 1, "ml", 0, 15, true);

            medicine.IsValidMedicine();
        }
        
        [TestMethod]
        public void CompareNullMedicineTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                            "Drops", 1, "ml", 0, 15, true);

            Medicine medicine1 = null;

            bool areEqual = medicine.Equals(medicine1);
            
            Assert.IsFalse(areEqual);
        }
        
        [TestMethod]
        public void CompareEqualCodeMedicineTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4564", "Clonapine", symptoms, 
                "Drops", 1, "ml", 0, 15, true);

            Medicine medicine1 = CreateMedicine(2, "AQHLO4564", "Clonazepam", symptoms, 
                "Drops", 1, "ml", 0, 15, true);

            bool areEqual = medicine.Equals(medicine1);
            
            Assert.IsTrue(areEqual);
        }
        
        [TestMethod]
        public void CompareDifferentCodeMedicineTest()
        {
            Medicine medicine = CreateMedicine(1, "AQHLO4567", "Clonapine", symptoms, 
                "Drops", 1, "ml", 0, 15, true);

            Medicine medicine1 = CreateMedicine(2, "AQHLO4564", "Clonazepam", symptoms, 
                "Drops", 1, "ml", 0, 15, true);

            bool areEqual = medicine.Equals(medicine1);
            
            Assert.IsFalse(areEqual);
        }
        
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
