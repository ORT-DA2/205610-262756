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
    public class RoleRepositoryTest
    {
        private BaseRepository<Role> _repository;
        private StartUpContext _context;
        private RoleRepository _roleRepository;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQLite);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Role>(_context);
            _roleRepository = new RoleRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetOneByExpressionNotExistTest()
        {
            Expression<Func<Role, bool>> expression = i => i.Permission.ToLower().Contains("Clonapine");
            var role = CreateRole();

            var retrievedRole = _roleRepository.GetOneByExpression(expression);
            
            Assert.IsNull(retrievedRole);
        }
        
        [TestMethod]
        public void GetOneByExpressionTest()
        {
            Role newRole = CreateRole();
            Expression<Func<Role, bool>> expression = m => m.Permission.ToLower().Contains("administrator");
            LoadRole(newRole);
            
            var retrievedRole = _roleRepository.GetOneByExpression(expression);
            
            Assert.IsNotNull(retrievedRole);
        }

        [TestMethod]
        public void InsertNewRole()
        {
            var role = CreateRole();
            LoadRole(role);
            var newRole = new Role()
            {
               Permission = "Owner"
            };

            _repository.InsertOne(newRole);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var roleInDb = _context.Roles.FirstOrDefault(m => m.Permission.Equals(newRole.Permission));
            Assert.IsNotNull(roleInDb);
        }


        private void LoadRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        private Role CreateRole()
        {
            return new()
            {
                Permission = "Administrator"
            };
        }
    }
}
