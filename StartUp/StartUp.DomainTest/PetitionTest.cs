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

        [TestMethod]
        public void NewPetitionTest()
        {
            Petition petition = CreatePetition(1, 3, "Valium");

            petition.IsValidPetition();
            
            Assert.IsNotNull(petition);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewPetitionWithMedicineCodeNullTest()
        {
            Petition petition = CreatePetition(1, 3, null);

            petition.IsValidPetition();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewPetitionWithAmount0Test()
        {
            Petition petition = CreatePetition(1, 0, "");

            petition.IsValidPetition();
        }

        [TestMethod]
        public void CompareNullPetitionTest()
        {
            Petition petition = CreatePetition(1, 3, "Oxa-B12");

            bool areEqual = petition.Equals(null);
            
            Assert.IsFalse(areEqual);
        }
        
        [TestMethod]
        public void CompareEqualPetitionTest()
        {
            Petition petition = CreatePetition(1, 3, "Tramadol");

            Petition petition1 = CreatePetition(1, 3, "Tramadol");

            bool areEqual = petition.Equals(petition1);
            
            Assert.IsTrue(areEqual);
        }
        
        [TestMethod]
        public void CompareDifferentPetitionTest()
        {
            Petition petition = CreatePetition(1, 3, "Silan-Compuesto");

            Petition petition1 = CreatePetition(2, 3, "Omeprazol");

            bool areEqual = petition.Equals(petition1);
            
            Assert.IsFalse(areEqual);
        }
        
        private Petition CreatePetition(int id, int amount, string med)
        {
            return new Petition()
            {
                Id = id,
                Amount = amount,
                MedicineCode = med
            };
        }
    }
}
