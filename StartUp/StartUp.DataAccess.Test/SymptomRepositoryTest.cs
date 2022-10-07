

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StartUp.DataAccess.Contexts;
using StartUp.DataAccess.Repositories;

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class SymptomRepositoryTest
    {
        private BaseRepository<Symptom> _repository;
        private StartUpContext _context;
        private SymptomRepository _symptomRepository;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQLite);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Symptom>(_context);
            _symptomRepository = new SymptomRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetOneByExpressionNotExistTest()
        {
            Expression<Func<Symptom, bool>> expression = i => i.SymptomDescription.ToLower().Contains("Clonapine");
            var symptom = CreateSymptom();

            var retrievedSymptom = _symptomRepository.GetOneByExpression(expression);
            
            Assert.IsNull(retrievedSymptom);
        }
        
        [TestMethod]
        public void GetOneByExpressionTest()
        {
            Symptom newSymptom = CreateSymptom();
            Expression<Func<Symptom, bool>> expression = m => m.SymptomDescription.ToLower().Contains("dolor");
            LoadSymptom(newSymptom);
            
            var retrievedSymptom = _symptomRepository.GetOneByExpression(expression);
            
            Assert.IsNotNull(retrievedSymptom);
        }

        [TestMethod]
        public void InsertNewSymptom()
        {
            var symptoms = CreateSymptom();
            LoadSymptom(symptoms);
            var newSymptom = new Symptom()
            {
                SymptomDescription = "dolor"
            };

            _repository.InsertOne(newSymptom);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var symptomInDb = _context.Symptoms.FirstOrDefault(s => s.Id.Equals(newSymptom.Id));
            Assert.IsNotNull(symptomInDb);
        }


        private void LoadSymptom(Symptom symptom)
        {
            _context.Symptoms.Add(symptom);
            _context.SaveChanges();
        }

        private Symptom CreateSymptom()
        {
            return  new()
            {
               SymptomDescription = "dolor"
            };
        }
    }
}
