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
    public class MedicineRepositoryTest
    {
        private BaseRepository<Medicine> _repository;
        private StartUpContext _context;
        private MedicineRepository _medicineRepository;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQLite);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Medicine>(_context);
            _medicineRepository = new MedicineRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetOneByExpressionNotExistTest()
        {
            Expression<Func<Medicine, bool>> expression = i => i.Name.ToLower().Contains("Clonapine");
            var medicine = CreateMedicine();

            var retrievedMedicine = _medicineRepository.GetOneByExpression(expression);
            
            Assert.IsNull(retrievedMedicine);
        }
        
        [TestMethod]
        public void GetOneByExpressionTest()
        {
            Medicine newMedicine = CreateMedicine();
            Expression<Func<Medicine, bool>> expression = m => m.Name.ToLower().Contains("perifar");
            LoadMedicine(newMedicine);
            
            var retrievedMedicine = _medicineRepository.GetOneByExpression(expression);
            
            Assert.IsNotNull(retrievedMedicine);
        }

        [TestMethod]
        public void InsertNewMedicine()
        {
            var medicine = CreateMedicine();
            LoadMedicine(medicine);
            var newMedicine = new Medicine()
            {
                Code = "safa11",
                Name = "perifar",
                Symptoms = new List<Symptom>(),
                Prescription = true,
                Presentation = "20 comprimidos",
                Amount = 200,
                Measure = "asa",
                Price = 200,
                Stock = 300,
            };

            _repository.InsertOne(newMedicine);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var medicineInDb = _context.Medicines.FirstOrDefault(m => m.Name.Equals(newMedicine.Name));
            Assert.IsNotNull(medicineInDb);
        }


        private void LoadMedicine(Medicine medicine)
        {
            _context.Medicines.Add(medicine);
            _context.SaveChanges();
        }

        private Medicine CreateMedicine()
        {
            return new()
            {
                Code = "safa11",
                Name = "perifar2",
                Symptoms = new List<Symptom>(),
                Prescription = false,
                Presentation = "10 comprimidos",
                Amount = 100,
                Measure = "a546sa",
                Price = 300,
                Stock = 100,
            };
        }
    }
}
