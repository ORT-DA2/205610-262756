using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StartUp.DataAccess.Contexts;
using StartUp.DataAccess.Repositories;

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        private BaseRepository<User> _repository;
        private StartUpContext _context;
        private UserRepository _userRepository;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQLite);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<User>(_context);
            _userRepository = new UserRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetOneByExpressionNotExistTest()
        {
            Expression<Func<User, bool>> expression = i => i.Email.ToLower().Contains("Clonapine");
            var user = CreateUser();

            var retrievedUser = _userRepository.GetOneByExpression(expression);
            
            Assert.IsNull(retrievedUser);
        }
        
        [TestMethod]
        public void GetOneByExpressionTest()
        {
            User newUser = CreateUser();
            Expression<Func<User, bool>> expression = m => m.Email.ToLower().Contains("paulaolivera2@gmail.com");
            LoadUser(newUser);
            
            var retrievedUser = _userRepository.GetOneByExpression(expression);
            
            Assert.IsNotNull(retrievedUser);
        }

        [TestMethod]
        public void InsertNewUser()
        {
            var users = CreateUser();
            LoadUser(users);
            Invitation invitation = new Invitation
            {
                UserName = "Paula",
                Pharmacy = null,
                Code = 123456,
                Rol = "Administrator"
            };
            var newUser = new User()
            {
                Email = "paulaolivera1995@gmail.com",
                Address = "carlos maria ramirez",
                Invitation = invitation,
                RegisterDate = DateTime.Now,
                Password = "123ss",
                Pharmacy = null,
                Roles = new Role
                {
                    Permission = invitation.Rol,
                }
            };

            _userRepository.InsertOne(newUser);
            _userRepository.Save();

            // Voy directo al contexto a buscarla
            var userInDb = _context.Users.FirstOrDefault(o => o.Email.Equals(newUser.Email));
            Assert.IsNotNull(userInDb);
        }


        private void LoadUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        private User CreateUser()
        {
            return new()
            {
                Email = "paulaolivera2@gmail.com",
                Address = "de la vega",
                Invitation = new Invitation(),
                RegisterDate = DateTime.Now,
                Password = "56899",
            };
        }
    }
}
