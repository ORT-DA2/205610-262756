using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;

namespace StartUp.DomainTest
{
    [TestClass]
    public class SymptomTest
    {
        [TestMethod]
        public void ValidOrFailPassesWithValidSymptom()
        {
            Symptom validSymptom = new Symptom();
            validSymptom.isValidSymptom();
        }
    }
}
