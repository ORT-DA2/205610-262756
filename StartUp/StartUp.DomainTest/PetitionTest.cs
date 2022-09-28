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
    public class PetitionTest
    {

        [TestInitialize]
        public void Setup()
        {
            
        }

        [TestCleanup]
        public void Cleanup()
        {

        }

        [TestMethod]
        public void NewPetitionTestOK()
        {
            Petition petition = new Petition();
            Assert.IsNotNull(petition);
        }
    }
}
