using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;

namespace StartUp.DomainTest
{
    [TestClass]
    public class PharmacyTest
    {
        Medicine medicineTest;
        Medicine medicine2;
        InvoiceLine line;
        Pharmacy pharmacy;
        Sale sale;
        List<Medicine> medicines;
        List<Request> requests;


        [TestInitialize]
        public void Setup()
        {
            medicines = new List<Medicine>();
            requests = new List<Request>();
            medicineTest = new Medicine();
            medicineTest.Code = "AA123";
            medicineTest.Stock = 10;
            medicine2 = new Medicine();
            medicine2.Code = "BB123";
            medicine2.Stock = 15;
            pharmacy = new Pharmacy();
            pharmacy.Stock = medicines;
            pharmacy.Stock.Add(medicineTest);
            pharmacy.Stock.Add(medicine2);
            sale = new Sale();
            sale.InvoiceLines = new List<InvoiceLine>();
            line = new InvoiceLine();
            line.Medicine = medicineTest;
            line.Amount = 5;
            sale.InvoiceLines.Add(line);
        }

        [TestMethod]
        public void NewPharmacyTestOK()
        {
            Pharmacy pharmacy1 = CreatePharmacy("a Pharmacy", "SolanoGarcia", medicines, requests);

            Assert.IsNotNull(pharmacy1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewPharmacyWithoutNameTest()
        {
            Pharmacy pharmacy1 = CreatePharmacy("", "SolanoGarcia", medicines, requests);

            pharmacy1.isValidPharmacy();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewPharmacyWithSpacesNameTest()
        {
            Pharmacy pharmacy1 = CreatePharmacy("    ", "SolanoGarcia", medicines, requests);

            pharmacy1.isValidPharmacy();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewPharmacyWithMoreThan50NameTest()
        {
            Pharmacy pharmacy1 = CreatePharmacy("012345678901234567890123456789012345678901234567890", "SolanoGarcia", medicines, requests);

            pharmacy1.isValidPharmacy();
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewPharmacyWithoutAddressTest()
        {
            Pharmacy pharmacy1 = CreatePharmacy("Una farmacia", "", medicines, requests);

            pharmacy1.isValidPharmacy();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewPharmacyWithSpacesAddressTest()
        {
            Pharmacy pharmacy1 = CreatePharmacy("Una farmacia", "      ", medicines, requests);
            
            pharmacy1.isValidPharmacy();
        }


        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewPharmacyWithoutMedicinesTest()
        {
            Pharmacy pharmacy1 = CreatePharmacy("Una farmacia", "SolanoGarcia", null, requests);

            pharmacy1.isValidPharmacy();
            
            Assert.IsNotNull(pharmacy1);
        }


        [TestMethod]
        public void NewPharmacyWithoutRequestTest()
        {
            Pharmacy pharmacy1 = CreatePharmacy("Una farmacia", "SolanoGarcia", medicines, null);

            pharmacy1.isValidPharmacy();
            
            Assert.IsNotNull(pharmacy1);
        }
        
        [TestMethod]
        public void ComparingEqualPharmacies()
        {
            Pharmacy pharmacy1 = CreatePharmacy("Una farmacia", "SolanoGarcia", medicines, requests);
            Pharmacy pharmacy = CreatePharmacy("Una farmacia", "SolanoGarcia", medicines, requests);
            
            bool areSame = pharmacy.Equals(pharmacy1);

            Assert.IsTrue(areSame);
        }
        
        [TestMethod]
        public void ComparingDiferentPharmacies()
        {
            Pharmacy pharmacy = CreatePharmacy("el tunel", "SolanoGarcia", medicines, requests);
            Pharmacy pharmacy1 = CreatePharmacy("Una farmacia", "SolanoGarcia", medicines, requests);

            bool areSame = pharmacy.Equals(pharmacy1);

            Assert.IsFalse(areSame);
        }

        [TestMethod]
        public void UpdateStockSecondTest()
        {
            Pharmacy pharmacy = CreatePharmacy("el tunel", "SolanoGarcia", medicines, requests);
            line.Amount = 10;
            //pharmacy.UpdateStock(sale);

            Assert.IsTrue(medicineTest.Stock == 0);
        }
        
        [TestMethod]
        public void UpdateStockTest()
        {
            Pharmacy pharmacy = CreatePharmacy("el tunel", "SolanoGarcia", medicines, requests);
            
            //pharmacy.UpdateStock(sale);

            Assert.IsTrue(medicineTest.Stock == 5);
        }

        private Pharmacy CreatePharmacy(string name, string address, List<Medicine> stock, List<Request> request)
        {
            Pharmacy pharmacy1 = new Pharmacy
            {
                Name = name, 
                Address = address,
                Stock = stock,
                Requests = request
            };

            return pharmacy1;
        }

    }
}
