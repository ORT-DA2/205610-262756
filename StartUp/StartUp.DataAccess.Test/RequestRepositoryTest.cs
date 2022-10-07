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
    public class RequestRepositoryTest
    {
        private BaseRepository<Request> _repository;
        private StartUpContext _context;
        private RequestRepository _requestRepository;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQLite);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Request>(_context);
            _requestRepository = new RequestRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }
        
        [TestMethod]
        public void GetOneByExpressionNotExistTest()
        {
            Expression<Func<Request, bool>> expression = req => req.State.ToLower().Contains("Rejected");
            var Request = CreateRequest();

            var retrievedRequest = _requestRepository.GetOneByExpression(expression);
            
            Assert.IsNull(retrievedRequest);
        }
        
        [TestMethod]
        public void GetOneByExpressionTest()
        {
            Request newRequest = CreateRequest();
            Expression<Func<Request, bool>> expression = req => req.State.ToLower().Contains("pending");
            LoadRequest(newRequest);
            
            var retrievedRequest = _requestRepository.GetOneByExpression(expression);
            
            Assert.IsNotNull(retrievedRequest);
        }
        
        [TestMethod]
        public void InsertNewRequest()
        {
            var requests = CreateRequest();
            LoadRequest(requests);
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


        private void LoadRequest(Request request)
        {
            _context.Requests.Add(request);
            _context.SaveChanges();
        }

        private Request CreateRequest()
        {
            return new()
            { 
                State = "Pending",
                Petitions = new List<Petition>()
            };
        }
    }
}
