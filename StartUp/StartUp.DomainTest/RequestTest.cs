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

        [TestMethod]
        public void NewRequestTest()
        {
           Request request = CreateRequest(1, petitionsList, true);
           
           request.isValidRequest();
           
           Assert.IsNotNull(request);
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewRequestWithNoRequestsTest()
        {
            Request request = CreateRequest(1, null, true);
           
            request.isValidRequest();
        }
        
        [TestMethod]
        public void CompareTwoRequestsTest()
        {
            Request request = CreateRequest(1, petitionsList, true);
            Request request1 = CreateRequest(1, petitionsList, true);

            bool areEqual = request.Equals(request1);
            
            Assert.IsTrue(areEqual);
        }
        
        [TestMethod]
        public void CompareTwoDiferentRequestTest()
        {
            Request request = CreateRequest(1, petitionsList, true);
            Request request1 = CreateRequest(2, petitionsList, true);

            bool areEqual = request.Equals(request1);
            
            Assert.IsFalse(areEqual);
        }
        
        [TestMethod]
        public void CompareNullRequestTest()
        {
            Request request = CreateRequest(1, petitionsList, true);
            Request request1 = null;

            bool areEqual = request.Equals(request1);
            
            Assert.IsFalse(areEqual);
        }
        
        private Request CreateRequest(int id, List<Petition> petit, bool state )
        {
            return new Request()
            {
                Id = id,
                Petitions = petit,
            };
        }
    }
}
