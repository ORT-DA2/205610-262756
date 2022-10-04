using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StartUp.DataAccess.Contexts;

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class RequestRepositoryTest
    {
        private BaseRepository<Request> _repository;
        private StartUpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQLite);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Request>(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllRequestReturnsAsExpected()
        {
            Expression<Func<Request, bool>> expression = r => r.State.ToString().ToLower().Contains("true");
            var requests = CreateRequests();
            var eligibleRequests = requests.Where(expression.Compile()).ToList();
            LoadRequests(requests);

            var retrievedRequests = _repository.GetAllByExpression(expression);
            CollectionAssert.AreEquivalent(eligibleRequests, retrievedRequests.ToList());
        }

        [TestMethod]
        public void InsertNewRequest()
        {
            var requests = CreateRequests();
            LoadRequests(requests);
            var newRequest = new Request()
            {
                Petitions = new List<Petition>()
            };

            _repository.InsertOne(newRequest);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var requestInDb = _context.Requests.FirstOrDefault(r => r.State.Equals(newRequest.State));
            Assert.IsNotNull(requestInDb);
        }


        private void LoadRequests(List<Request> requests)
        {
            requests.ForEach(r => _context.Requests.Add(r));
            _context.SaveChanges();
        }

        private List<Request> CreateRequests()
        {
            return new List<Request>()
        {
            new()
            {
               Petitions = new List<Petition>()
            },
            new()
            {
                Petitions = new List<Petition>()
            }
        };
        }
    }
}
