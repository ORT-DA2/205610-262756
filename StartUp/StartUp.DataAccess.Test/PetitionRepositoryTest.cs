using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StartUp.DataAccess.Contexts;
using StartUp.DataAccess.Repositories;

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class PetitionRepositoryTest
    {
        private BaseRepository<Petition> _repository;
        private StartUpContext _context;
        private PetitionRepository _petitionRepository;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQLite);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Petition>(_context);
            _petitionRepository = new PetitionRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetOneByExpressionNotExistTest()
        {
            Expression<Func<Petition, bool>> expression = i => i.MedicineCode.ToLower().Contains("Clonapine");
            var petition = CreatePetition();

            var retrievedPetition = _petitionRepository.GetOneByExpression(expression);
            
            Assert.IsNull(retrievedPetition);
        }
        
        [TestMethod]
        public void GetOneByExpressionTest()
        {
            Petition newPetition = CreatePetition();
            Expression<Func<Petition, bool>> expression = p => p.MedicineCode.ToLower().Contains("jhkjdh231");
            LoadPetition(newPetition);
            
            var retrievedPetition = _petitionRepository.GetOneByExpression(expression);
            
            Assert.IsNotNull(retrievedPetition);
        }

        [TestMethod]
        public void InsertNewPetition()
        {
            var petitions = CreatePetition();
            LoadPetition(petitions);
            var newPetition = new Petition()
            {
                Amount = 0,
                MedicineCode = "petitionCode"
            };

            _repository.InsertOne(newPetition);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var petitionInDb = _context.Petitions.FirstOrDefault(p => p.MedicineCode.Equals(newPetition.MedicineCode));
            Assert.IsNotNull(petitionInDb);
        }


        private void LoadPetition(Petition petition)
        {
            _context.Petitions.Add(petition);
            _context.SaveChanges();
        }

        private Petition CreatePetition()
        {
            return new()
            {
                Amount = 0,
                MedicineCode = "jhkjdh231"
            };
        }
    }
}
