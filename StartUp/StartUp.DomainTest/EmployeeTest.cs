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
    public class EmployeeTest
    {
        Pharmacy pharmacy;

        [TestInitialize]
        public void Setup()
        {
            pharmacy = new Pharmacy();
        }

        [TestCleanup]
        public void Cleanup()
        {

        }

        [TestMethod]
        public void NewEmployeeTestOK()
        {
            Employee employee = new Employee();
            employee.Pharmacy = pharmacy;

            Assert.IsNotNull(employee);
        }
    }
}
