using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Linq.Expressions;
using StartUp.DataAccess.Contexts;
using StartUp.DataAccess.Repositories;
using StartUp.Domain.Entities;

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class SessionRepositoryTest
    {
        private BaseRepository<Session> _repository;
        private StartUpContext _context;
        private SessionRepository _sessionRepository;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQLite);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Session>(_context);
            _sessionRepository = new SessionRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetOneByExpressionNotExistTest()
        {
            Expression<Func<Session, bool>> expression = i => i.Username.ToLower().Contains("Clonapine");
            var session = CreateSession();

            var retrievedSession = _sessionRepository.GetOneByExpression(expression);
            
            Assert.IsNull(retrievedSession);
        }
        
        [TestMethod]
        public void GetOneByExpressionTest()
        {
            Session newSession = CreateSession();
            Expression<Func<Session, bool>> expression = m => m.Username.ToLower().Contains("julia");
            LoadSession(newSession);
            
            var retrievedSession = _sessionRepository.GetOneByExpression(expression);
            
            Assert.IsNotNull(retrievedSession);
        }

        [TestMethod]
        public void InsertNewSession()
        {
            var session = CreateSession();
            LoadSession(session);
            var newSession = new Session()
            {
               Username = "Luis"
            };

            _repository.InsertOne(newSession);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var sessionInDb = _context.Session.FirstOrDefault(m => m.Username.Equals(newSession.Username));
            Assert.IsNotNull(sessionInDb);
        }


        private void LoadSession(Session session)
        {
            _context.Session.Add(session);
            _context.SaveChanges();
        }

        private Session CreateSession()
        {
            return new()
            {
                Username = "Julia"
            };
        }
    }
}
