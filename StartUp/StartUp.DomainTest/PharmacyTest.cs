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
        [ExpectedException(typeof(StartUp.Exceptions.InputException))]
        public void NewPharmacyTestNameNull()
        {
            Pharmacy pharmacy = new Pharmacy(null, "Carlos Maria Ramirez 106", null, requests);
        }

    }
}
