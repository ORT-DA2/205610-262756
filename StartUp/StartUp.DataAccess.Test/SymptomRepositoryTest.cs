

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class SymptomRepositoryTest
    {
        private BaseRepository<Symptom> _repository;
        private StartUpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQL);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Symptom>(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllSymptomReturnsAsExpected()
        {
            Expression<Func<Symptom, bool>> expression = s => s.Id.ToString().Contains("1");
            var symptoms = CreateSymptoms();
            var eligibleSymptoms = symptoms.Where(expression.Compile()).ToList();
            LoadSymptoms(symptoms);

            var retrievedSymptoms = _repository.GetAllByExpression(expression);
            CollectionAssert.AreEquivalent(eligibleSymptoms, retrievedSymptoms.ToList());
        }

        [TestMethod]
        public void InsertNewSymptom()
        {
            var symptoms = CreateSymptoms();
            LoadSymptoms(symptoms);
            var newSymptom = new Symptom()
            {
                SymptomDescription = "desc"
            };

            _repository.InsertOne(newSymptom);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var symptomInDb = _context.Symptoms.FirstOrDefault(s => s.Id.Equals(newSymptom.Id));
            Assert.IsNotNull(symptomInDb);
        }


        private void LoadSymptoms(List<Symptom> symptoms)
        {
            symptoms.ForEach(s => _context.Symptoms.Add(s));
            _context.SaveChanges();
        }

        private List<Symptom> CreateSymptoms()
        {
            return new List<Symptom>()
        {
            new()
            {
               SymptomDescription = "desc"
            },
            new()
            {
                SymptomDescription = "sdasf"
            }
        };
        }
    }
}
