using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.DataAccess.Repositories;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.DataAccess.Test
{
    public class PharmacyRepositoryTest
    {
        private PharmacyRepository _repository;
        private StartUpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.Memory);
            _repository = new PharmacyRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllPharmacy_DbWithPharmacy_ReturnsMatchingPharmacy()
        {
            LoadSeeds();
            var pharmacy = CreateDummyPharmacy();

            var retrievedPharmacy = _repository.GetAllByExpression(p => p.Name.ToLower().Contains("La nueva"));

            CollectionAssert.AreEquivalent(pharmacy, retrievedPharmacy.ToList());
        }

        [TestMethod]
        public void InsertPharmacy_PharmacyNotExists_ReturnsVoid()
        {
            LoadSeeds();/*
            var actorInDb = _context.Actors.First();
            var newMovie = new Movie()
            {
                Title = "Interstellar",
                Description = "Muy buena",
                Actors = new List<Actor>() { actorInDb }
            };

            _repository.InsertOne(newMovie);
            _context.SaveChanges();

            var movieInDb = _context.Movies.First(m => m.Title.Equals(newMovie.Title));
            Assert.AreEqual(movieInDb, newMovie);*/
        }


        private void LoadSeeds()
        {
            /*var actorJuan = new Actor()
            {
                FirstName = "Juan Carlos"
            };
            var movies = CreateDummyMovies();
            movies.First().Actors.Add(actorJuan);
            actorJuan.Movies.Add(movies.First());

            movies.ForEach(m => _context.Movies.Add(m));*/
            _context.SaveChanges();
        }

        private List<Pharmacy> CreateDummyPharmacy()
        {
            return new List<Pharmacy>()
        {
            new()
            {
                Name = "La nueva",
                Address = "barrios amorin 1544"
            },
            new()
            {
                Name = "La vieja",
                Address = "baldomir 1547"
            }
        };
        }
    }
}
