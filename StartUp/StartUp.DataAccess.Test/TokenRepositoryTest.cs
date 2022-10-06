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
    public class TokenAccessRepositoryTest
    {
        private BaseRepository<TokenAccess> _repository;
        private StartUpContext _context;
        private TokenRepository _tokenRepository;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQLite);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<TokenAccess>(_context);
            _tokenRepository = new TokenRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetOneByExpressionNotExistTest()
        {
            Expression<Func<TokenAccess, bool>> expression = i => i.User.Email.ToLower().Contains("Clonapine");
            var token = CreateTokenAccess();

            var retrievedTokenAccess = _tokenRepository.GetOneByExpression(expression);
            
            Assert.IsNull(retrievedTokenAccess);
        }
        
        [TestMethod]
        public void GetOneByExpressionTest()
        {
            TokenAccess newTokenAccess = CreateTokenAccess();
            Expression<Func<TokenAccess, bool>> expression = m => m.User.Email.ToLower().Contains("bel@gmail");
            LoadTokenAccess(newTokenAccess);
            
            var retrievedTokenAccess = _tokenRepository.GetOneByExpression(expression);
            
            Assert.IsNotNull(retrievedTokenAccess);
        }

        [TestMethod]
        public void InsertNewTokenAccess()
        {
            var token = CreateTokenAccess();
            LoadTokenAccess(token);
            var newTokenAccess = new TokenAccess()
            {
                Token = new Guid(),
                User = new User
                {
                    Email = "pau@gmail"
                }
            };

            _repository.InsertOne(newTokenAccess);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var tokenInDb = _context.TokenAccess.FirstOrDefault(m => m.User.Email.Equals(newTokenAccess.User.Email));
            Assert.IsNotNull(tokenInDb);
        }


        private void LoadTokenAccess(TokenAccess token)
        {
            _context.TokenAccess.Add(token);
            _context.SaveChanges();
        }

        private TokenAccess CreateTokenAccess()
        {
            return new()
            {
                Token = new Guid(),
                User = new User
                {
                    Email = "bel@gmail"
                }
            };
        }
    }
}
