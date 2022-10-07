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
using StartUp.DataAccess.Contexts;

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class PharmacyRepositoryTest
    {
        private BaseRepository<Pharmacy> _repository;
        private StartUpContext _context;
        private PharmacyRepository _pharmacyRepository;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQLite);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Pharmacy>(_context);
            _pharmacyRepository = new PharmacyRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetOneByExpressionNotExistTest()
        {
            Expression<Func<Pharmacy, bool>> expression = i => i.Name.ToLower().Contains("Clonapine");
            var pharmacy = CreatePharmacy();

            var retrievedPharmacy = _pharmacyRepository.GetOneByExpression(expression);
            
            Assert.IsNull(retrievedPharmacy);
        }
        
        [TestMethod]
        public void GetOneByExpressionTest()
        {
            Pharmacy newPharmacy = CreatePharmacy();
            Expression<Func<Pharmacy, bool>> expression = ph => ph.Name.ToLower().Contains("el faro");
            LoadPharmacy(newPharmacy);
            
            var retrievedPharmacy = _pharmacyRepository.GetOneByExpression(expression);
            
            Assert.IsNotNull(retrievedPharmacy);
        }

        [TestMethod]
        public void InsertNewPharmacy()
        {
            var pharmacies = CreatePharmacy();
            LoadPharmacy(pharmacies);
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


        private void LoadPharmacy(Pharmacy pharmacy)
        {
            _context.Pharmacies.Add(pharmacy);
            _context.SaveChanges();
        }

        private Pharmacy CreatePharmacy()
        {
            return new()
            {
                Name = "el faro",
                Address = "18 de julio",
                Stock = new List<Medicine>(),
                Requests = new List<Request>()
            };
        }
    }
}
