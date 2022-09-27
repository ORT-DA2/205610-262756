using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.DomainTest
{
    public class OwnerTest
    {
        [TestMethod]
        public void ValidPassesWithValidOwner()
        {
            Owner validOwner = new Owner() { Email = "unemail@gmail.com", Password = "123456", Address = "una direccion" };
            validOwner.isValidOwner();
        }
    }
}
