using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        private BaseRepository<User> _repository;
        private StartUpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQL);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<User>(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllUserReturnsAsExpected()
        {
            Expression<Func<User, bool>> expression = o => o.Email.ToLower().Contains("paulaolivera1995@gmail.com");
            var users = CreateUsers();
            var eligibleUsers = users.Where(expression.Compile()).ToList();
            LoadUsers(users);

            var retrievedUsers = _repository.GetAllByExpression(expression);
            CollectionAssert.AreEquivalent(eligibleUsers, retrievedUsers.ToList());
            
        }

        [TestMethod]
        public void InsertNewUser()
        {
            var users = CreateUsers();
            LoadUsers(users);
            var newUser = new User()
            {
                Email = "paulaolivera1995@gmail.com",
                Address = "carlos maria ramirez",
                Invitation = new Invitation(),
                RegisterDate = DateTime.Now,
                Password = "123ss",
            };

            _repository.InsertOne(newUser);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var userInDb = _context.Users.FirstOrDefault(o => o.Email.Equals(newUser.Email));
            Assert.IsNotNull(userInDb);
        }


        private void LoadUsers(List<User> users)
        {
            users.ForEach(o => _context.Users.Add(o));
            _context.SaveChanges();
        }

        private List<User> CreateUsers()
        {
            return new List<User>()
        {
            new()
            {
                Email = "paulaolivera2@gmail.com",
                Address = "de la vega",
                Invitation = new Invitation(),
                RegisterDate = DateTime.Now,
                Password = "56899",
            },
            new()
            {
                Email = "paulaolivera3@gmail.com",
                Address = "fernandez crespo",
                Invitation = new Invitation(),
                RegisterDate = DateTime.Now,
                Password = "54s6dfadf",
            }
        };
        }
    }
}
