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
        public void NewSymptomTest()
        {
            Symptom symptom = CreateSymptom(1, "fiebre");

            symptom.IsValidSymptom();

            Assert.IsNotNull(symptom);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewSymptomWithNoDescriptionTest()
        {
            Symptom symptom = CreateSymptom(1, "");

            symptom.IsValidSymptom();

            Assert.IsNotNull(symptom);
        }

        [TestMethod]
        public void CompareNullSymptomTest()
        {
            Symptom symptom = CreateSymptom(1, "fiebre");

            Symptom symptom2 = null;

            bool areEqual = symptom.Equals(symptom2);

            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void CompareEqualIdSymptomTest()
        {
            Symptom symptom = CreateSymptom(1, "fiebre");

            Symptom symptom2 = CreateSymptom(2, "tos");

            bool areEqual = symptom.Equals(symptom2);

            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void CompareDifferentIdSymptomTest()
        {
            Symptom symptom = CreateSymptom(1, "fiebre");

            Symptom symptom2 = CreateSymptom(2, "tos");

            bool areEqual = symptom.Equals(symptom2);

            Assert.IsTrue(areEqual);
        }

        public void NewSymptomTestOK()
        {
            Symptom symptom = new Symptom();

            Assert.IsNotNull(symptom);
        }

        private Symptom CreateSymptom(int id, string description)
        {
            return new Symptom()
            {
                Id = id,
                SymptomDescription = description,
            };
        }
    }
}
