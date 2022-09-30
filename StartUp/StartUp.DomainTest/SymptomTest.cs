using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;

namespace StartUp.DomainTest
{
    [TestClass]
    public class SymptomTest
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
        public void ValidOrFailPassesWithValidSymptom()
        {
            Symptom validSymptom = new Symptom();
            validSymptom.isValidSymptom();
        }

        public void NewSymptomTestOK()
        {
            Symptom symptom = new Symptom();

            Assert.IsNotNull(symptom);
        }
    }
}
