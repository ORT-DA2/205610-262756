using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class MedicineRepositoryTest
    {
        private BaseRepository<Medicine> _repository;
        private StartUpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQL);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Medicine>(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllAdministratorReturnsAsExpected()
        {
            Expression<Func<Medicine, bool>> expression = m => m.Name.ToLower().Contains("perifar");
            var medicines = CreateMedicines();
            var eligibleMedicines = medicines.Where(expression.Compile()).ToList();
            LoadMedicines(medicines);

            var retrievedMedicines = _repository.GetAllExpression(expression);
            CollectionAssert.AreEquivalent(eligibleMedicines, retrievedMedicines.ToList());
        }

        [TestMethod]
        public void InsertNewMedicine()
        {
            var medicines = CreateMedicines();
            LoadMedicines(medicines);
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


        private void LoadMedicines(List<Medicine> medicines)
        {
            medicines.ForEach(m => _context.Medicines.Add(m));
            _context.SaveChanges();
        }

        private List<Medicine> CreateMedicines()
        {
            return new List<Medicine>()
        {
            new()
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
            },
            new()
            {
                Code = "safa145661",
                Name = "perifar3",
                Symptoms = new List<Symptom>(),
                Prescription = true,
                Presentation = "25 comprimidos",
                Amount = 400,
                Measure = "as997a",
                Price = 100,
                Stock = 200,
            }
        };
        }
    }
}
