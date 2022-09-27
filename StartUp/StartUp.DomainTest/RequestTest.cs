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
    public class RequestTest
    {
        List<Petition> petitionsList;

        [TestInitialize]
        public void Setup()
        {
            petitionsList = new List<Petition>();
        }

        [TestCleanup]
        public void Cleanup()
        {

        }

        [TestMethod]
        public void NewRequestTestOK()
        {
            Request request = new Request();
            request.Petitions = new List<Petition>();

            Assert.IsNotNull(request);
        }
    }
}
