using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.BusinessLogic;
using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace StartUp.BusinessLogicTest
{
    [TestClass]
    public class SymptomServiceTest
    {
        private Mock<IDataAccess.IRepository<Symptom>> _repoMock;
        private SymptomService _service;

        [TestInitialize]
        public void SetUp()
        {
            _repoMock = new Mock<IDataAccess.IRepository<Symptom>>(MockBehavior.Strict);
            _service = new SymptomService(_repoMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _repoMock.VerifyAll();
        }

        [TestMethod]
        public void GetSpecificSymptomTest()
        {
            var symptom = CreateSymptom(1, "fiebre");
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Symptom, bool>>>())).Returns(symptom);

            var retrievedSymptom = _service.GetSpecificSymptom(symptom.Id);

            Assert.AreEqual(symptom.Id, retrievedSymptom.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetSpecificNullSymptomTest()
        {
            var symptom = CreateSymptom(1, "fiebre");

            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Symptom, bool>>>())).Returns((Symptom)null);

            _service.GetSpecificSymptom(symptom.Id);
        }

        [TestMethod]
        public void GetAllSymptomTest()
        {
            List<Symptom> dummySymptom = GenerateDummySymptom();
            _repoMock.Setup(repo => repo.GetAllByExpression(It.IsAny<Expression<Func<Symptom, bool>>>())).Returns(dummySymptom);
            SymptomSearchCriteria searchCriteria = new SymptomSearchCriteria();

            var retrievedSymptom = _service.GetAllSymptom(searchCriteria);

            CollectionAssert.AreEqual(dummySymptom, retrievedSymptom);
        }

        [TestMethod]
        public void UpdateSymptomTest()
        {
            var symptom = CreateSymptom(1, "fiebre");
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Symptom, bool>>>())).Returns(symptom);
            Symptom updateData = CreateSymptom(symptom.Id, "tos");

            _repoMock.Setup(repo => repo.UpdateOne(symptom));
            _repoMock.Setup(repo => repo.Save());

            Symptom updatedSymptom = _service.UpdateSymptom(symptom.Id, updateData);

            Assert.AreEqual(updatedSymptom, symptom);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteNotExistingSymptomTest()
        {
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Symptom, bool>>>())).Returns((Symptom)null);

            _service.DeleteSymptom(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteSymptomTest()
        {
            var symptom = CreateSymptom(1, "fiebre");
            _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Symptom, bool>>>())).Returns(symptom).Returns((Symptom)null);
            _repoMock.Setup(repo => repo.DeleteOne(symptom));
            _repoMock.Setup(repo => repo.Save());

            _service.DeleteSymptom(symptom.Id);

            _service.GetSpecificSymptom(symptom.Id);
        }

        [TestMethod]
        public void CreateSymptomTest()
        {
            var symptom = CreateSymptom(1, "fiebre");
            _repoMock.Setup(repo => repo.InsertOne(symptom));
            _repoMock.Setup(repo => repo.Save());

            Symptom newSymptom = _service.CreateSymptom(symptom);

            Assert.AreEqual(newSymptom, symptom);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void CreateInvalidSymptomTest()
        {
            var symptom = CreateSymptom(1, null);

            _service.CreateSymptom(symptom);
        }

        private List<Symptom> GenerateDummySymptom() => new List<Symptom>()
        {
            new Symptom() { Id= 1, SymptomDescription = "description 1"},
            new Symptom() { Id= 2,  SymptomDescription = "description 2"}
        };

        private Symptom CreateSymptom(int id, string description)
        {
            return new Symptom
            {
                Id = id,
                SymptomDescription = description
            };
        }
    }
}
