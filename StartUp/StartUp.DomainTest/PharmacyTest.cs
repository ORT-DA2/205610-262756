using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System.Collections.Generic;

namespace StartUp.DomainTest
{
    [TestClass]
    public class PharmacyTest
    {
        Medicine medicine;
        List<Medicine> medicines;
        Request request;
        List<Request> requests;


        [TestInitialize]
        public void Setup()
        {
            medicine = new Medicine();
            medicines = new List<Medicine>();
            medicines.Add(medicine);
            request = new Request();
            requests = new List<Request>();   
            requests.Add(request);
        }

        [TestCleanup]
        public void Cleanup()
        {

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
        }


        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewPharmacyWithoutRequestTest()
        {
            Pharmacy pharmacy1 = CreatePharmacy("Una farmacia", "SolanoGarcia", medicines, null);

            pharmacy1.isValidPharmacy();
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
