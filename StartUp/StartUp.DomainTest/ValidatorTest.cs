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
        public void ValidateStringNotEqualsTest()
        {
            validator.ValidateStringEquals(stringTest, "79878", "Are not equal");
        }

        [TestMethod]
        public void ValidateStringEqualsTest()
        {
            validator.ValidateStringEquals(stringTest, "", "Are not equal");
        }

    }
}
