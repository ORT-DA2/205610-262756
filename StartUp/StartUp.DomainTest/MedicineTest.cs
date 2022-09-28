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

        [TestCleanup]
        public void Cleanup()
        {

        }

        [TestMethod]
        public void NewMedicineTestOK()
        {
            Medicine medicine = new Medicine();

            Assert.IsNotNull(medicine);
        }
    }
}
