using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace StartUp.DomainTest
{
    [TestClass]
    public class ValidatorTest
    {
        string stringTest;
        Validator validator;

        [TestInitialize]
        public void Setup()
        {
            stringTest = "";
            validator = new Validator();
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateStringAllSpacesTest()
        {
            stringTest = "        ";
            validator.ValidateString(stringTest, "String empty");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateStringEmptyTest()
        {
            validator.ValidateString(stringTest, "String empty");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateStringLength0Test()
        {
            validator.ValidateString(stringTest, "String empty");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateLengthStringTest()
        {
            stringTest = "hola";
            validator.ValidateLengthString(stringTest, "String empty", 0);
        }

        [TestMethod]
        public void ValidateLengthStringOkTest()
        {
            string stringTest = "string test";
            validator.ValidateLengthString(stringTest, "String empty", 15);
        }

        [TestMethod]
        public void ValidateAmountTest()
        {
            validator.ValidateAmount(20, 10, "Exception");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateAmountWithErrorTest()
        {
            validator.ValidateAmount(0, 10, "Exception");
        }

        [TestMethod]
        public void ValidateContainsRolesCorrectTest()
        {
            string permissions = "padre";
            string roles = "padre,madre,hermano";
            validator.ValidateContainsRolesCorrect(roles, permissions, "Exception");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateContainsRolesIncorrectTest()
        {
            string permissions = "primo";
            string roles = "padre,madre,hermano";
            validator.ValidateContainsRolesCorrect(roles, permissions, "Exception");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateInvitationNullTest()
        {
            validator.ValidateInvitationNotNull(null, "Is null");
        }

        [TestMethod]
        public void ValidateInvitationNotNullTest()
        {
            Invitation inv = new Invitation();
            validator.ValidateInvitationNotNull(inv, "Is null");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateListPharmacyIsNullTest()
        {
            validator.ValidateListPharmacyNotNull(null, "Is null");
        }

        [TestMethod]
        public void ValidateListPharmacyIsNotNullTest()
        {
            List<Pharmacy> pharmacies = new List<Pharmacy>();
            validator.ValidateListPharmacyNotNull(pharmacies, "Is null");
        }

        [TestMethod]
        public void ValidateMedicineListNotNullTest()
        {
            List<Medicine> medicines = new List<Medicine>();
            validator.ValidateMedicineListNotNull(medicines, "Is null");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateMedicineListNullTest()
        {
            validator.ValidateMedicineListNotNull(null, "Is null");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateMedicineNullTest()
        {
            validator.ValidateMedicineNotNull(null, "Is null");
        }

        [TestMethod]
        public void ValidateMedicineIsNotNullTest()
        {
            Medicine medicine = new Medicine();
            validator.ValidateMedicineNotNull(medicine, "Is null");
        }

        [TestMethod]
        public void ValidatePasswordValidTest()
        {
            string password = "password?.(asa";
            validator.ValidatePasswordValid(password, "Not valid", 15);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidatePasswordInvalidTest()
        {
            string password = "p";
            validator.ValidatePasswordValid(password, "Not valid", 5);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidatePasswordAllSpacesTest()
        {
            string password = "  ";
            validator.ValidatePasswordValid(password, "Not valid", 5);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidatePasswordInvalidWithoutCharSpecialTest()
        {
            string password = "password";
            validator.ValidatePasswordValid(password, "Not valid", 5);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidatePetitionNullTest()
        {
            validator.ValidatePetitionNotNull(null, "Is null");
        }

        [TestMethod]
        public void ValidatePetitionIsNotNullTest()
        {
            Petition petition = new Petition(); 
            validator.ValidatePetitionNotNull(petition, "Is null");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateRequestNullTest()
        {
            validator.ValidateRequestNotNull(null, "Is null");
        }

        [TestMethod]
        public void ValidateRequestIsNotNullTest()
        {
            Request request = new Request();
            validator.ValidateRequestNotNull(request, "Is null");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateRoleNullTest()
        {
            validator.ValidateRoleIsNotNull(null, "Is null");
        }

        [TestMethod]
        public void ValidateRoleIsNotNullTest()
        {
            Role role = new Role();
            validator.ValidateRoleIsNotNull(role, "Is null");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.ResourceNotFoundException))]
        public void ValidateSaleNullTest()
        {
            validator.ValidateSaleNotNull(null, "Is null");
        }

        [TestMethod]
        public void ValidateSaleIsNotNullTest()
        {
            Sale sale = new Sale();
            validator.ValidateSaleNotNull(sale, "Is null");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidatePharmacyNullTest()
        {
            validator.ValidatePharmacyNotNull(null, "Is null");
        }

        [TestMethod]
        public void ValidatePharmacyIsNotNullTest()
        {
            Pharmacy pharmacy = new Pharmacy();
            validator.ValidatePharmacyNotNull(pharmacy, "Is null");
        }


        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateStringNotEqualsTest()
        {
            validator.ValidateStringEquals(stringTest, "79878", "Are not equal");
        }

        [TestMethod]
        public void ValidateStringEqualsTest()
        {
            validator.ValidateStringEquals(stringTest, "", "Are not equal");
        }

        [TestMethod]
        public void ValidateTokenAccessTest()
        {
            TokenAccess token = new TokenAccess();
            validator.ValidateTokenAccess(token, "Is empty");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateTokenAccessNullTest()
        {
            validator.ValidateTokenAccess(null, "Is null");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateUserTest()
        {
            User user = new User();
            validator.ValidateUserNull(user, "Is empty");
        }

        [TestMethod]
        public void ValidateUserNullTest()
        {
            validator.ValidateUserNull(null, "Is null");
        }

        [TestMethod]
        public void ValidateSessionTest()
        {
            Session session = new Session();
            validator.ValidateSessionNotNull(session, "Is empty");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void ValidateSessionNullTest()
        {
            validator.ValidateSessionNotNull(null, "Is null");
        }

    }
}
