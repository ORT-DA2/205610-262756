using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.DataAccess.Repositories;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.DataAccess.Test
{
    public class PharmacyRepositoryTest
    {
        private BaseRepository<Pharmacy> _repository;
        private StartUpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQL);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Pharmacy>(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllPharmacyReturnsAsExpected()
        {
            Expression<Func<Pharmacy, bool>> expression = p => p.Name.ToLower().Contains("el faro");
            var pharmacies = CreatePharmacies();
            var eligiblePharmacies = pharmacies.Where(expression.Compile()).ToList();
            LoadPharmacies(pharmacies);

            var retrievedPharmacies = _repository.GetAllByExpression(expression);
            CollectionAssert.AreEquivalent(eligiblePharmacies, retrievedPharmacies.ToList());
        }

        [TestMethod]
        public void InsertNewPharmacy()
        {
            var pharmacies = CreatePharmacies();
            LoadPharmacies(pharmacies);
            var newPharmacy = new Pharmacy()
            {
                Name = "la isla",
                Address = "arenal grande",
                Stock = new List<Medicine>(),
                Requests = new List<Request>(),
            };

            _repository.InsertOne(newPharmacy);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var pharmaciesInDb = _context.Pharmacies.FirstOrDefault(p => p.Name.Equals(newPharmacy.Name));
            Assert.IsNotNull(pharmaciesInDb);
        }


        private void LoadPharmacies(List<Pharmacy> pharmacies)
        {
            pharmacies.ForEach(p => _context.Pharmacies.Add(p));
            _context.SaveChanges();
        }

        private List<Pharmacy> CreatePharmacies()
        {
            return new List<Pharmacy>()
        {
            new()
            {
                Name = "el faro",
                Address = "18 de julio",
                Stock = new List<Medicine>(),
                Requests = new List<Request>()
            },
            new()
            {
                Name = "farmashop",
                Address = "bv artigas",
                Stock = new List<Medicine>(),
                Requests = new List<Request>()
            }
        };
        }
    }
}
