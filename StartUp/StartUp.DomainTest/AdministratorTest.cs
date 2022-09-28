using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;

namespace StartUp.DomainTest
{
    [TestClass]
    public class AdministratorTest
    {
        [TestMethod]
        public void ValidOrFailPassesWithValidAdministrator()
        {
            Administrator validAdmin = new Administrator() { Email = "unemail@gmail.com", Password = "123456", Address = "una direccion"};
            validAdmin.isValidAdministrator();
        }
    }
}
