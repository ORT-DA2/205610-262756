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
            Pharmacy pharmacy = new Pharmacy("Abichuela", "Carlos Maria Ramirez 106", medicines, requests);

            Assert.IsNotNull(pharmacy);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewPharmacyWithoutNameTest()
        {
            Pharmacy pharmacy = new Pharmacy("", "Carlos Maria Ramirez 106", medicines, requests);

            pharmacy.isValidPharmacy();
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewPharmacyWithoutAddressTest()
        {
            Pharmacy pharmacy = new Pharmacy("Una farmacia", "", medicines, requests);

            pharmacy.isValidPharmacy();
        }


        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewPharmacyWithoutMedicinesTest()
        {
            Pharmacy pharmacy = new Pharmacy("Una farmacia", "18 de julio", null, requests);

            pharmacy.isValidPharmacy();
        }


        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewPharmacyWithoutRequestTest()
        {
            Pharmacy pharmacy = new Pharmacy("Una farmacia", "Arenal grande", medicines, null);

            pharmacy.isValidPharmacy();
        }

    }
}
