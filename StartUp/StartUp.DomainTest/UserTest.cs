using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using StartUp.Domain.Entities;

namespace StartUp.DomainTest
{
    [TestClass]
    public class UserTest
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
        public void NewUserTestOK()
        {
            User user = new User();
            user.Pharmacy = pharmacy;

            Assert.IsNotNull(user);
        }
    }
}
