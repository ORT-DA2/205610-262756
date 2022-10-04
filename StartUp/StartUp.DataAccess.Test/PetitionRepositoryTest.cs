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

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class PetitionRepositoryTest
    {
        private BaseRepository<Petition> _repository;
        private StartUpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQLite);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Petition>(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllPetitionReturnsAsExpected()
        {
            Expression<Func<Petition, bool>> expression = p => p.MedicineCode.ToLower().Contains("medicineCode");
            var petitions = CreatePetitions();
            var eligiblePetitions = petitions.Where(expression.Compile()).ToList();
            LoadPetitions(petitions);

            var retrievedPetitions = _repository.GetAllByExpression(expression);
            CollectionAssert.AreEquivalent(eligiblePetitions, retrievedPetitions.ToList());
        }

        [TestMethod]
        public void InsertNewPetition()
        {
            var petitions = CreatePetitions();
            LoadPetitions(petitions);
            var newPetition = new Petition()
            {
                Amount = 0,
                MedicineCode = "medicineCode"
            };

            _repository.InsertOne(newPetition);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var petitionInDb = _context.Petitions.FirstOrDefault(p => p.MedicineCode.Equals(newPetition.MedicineCode));
            Assert.IsNotNull(petitionInDb);
        }


        private void LoadPetitions(List<Petition> petitions)
        {
            petitions.ForEach(p => _context.Petitions.Add(p));
            _context.SaveChanges();
        }

        private List<Petition> CreatePetitions()
        {
            return new List<Petition>()
        {
            new()
            {
                Amount = 0,
                MedicineCode = "medicineCode"
            },
            new()
            {
                Amount = 0,
                MedicineCode = "medicineCode"
            }
        };
        }
    }
}
