using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.BusinessLogic;
using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace StartUp.BusinessLogicTest
{
    [TestClass]
    public class RoleServiceTest
    {
        private Mock<IDataAccess.IRepository<Role>> _repoMock;
        private RoleService _service;

        [TestInitialize]
        public void SetUp()
        {
            _repoMock = new Mock<IDataAccess.IRepository<Role>>(MockBehavior.Strict);
            _service = new RoleService(_repoMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _repoMock.VerifyAll();
        }

        [TestMethod]
        public void GetSpecificRoleTest()
        {
            var role = CreateRole(1, "administrator");
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Role, bool>>>())).Returns(role);

            var retrievedRole = _service.GetSpecificRole(role.Id);

            Assert.AreEqual(role.Id, retrievedRole.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetSpecificNullRoleTest()
        {
            var role = CreateRole(1, "administrator");

            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Role, bool>>>())).Returns((Role)null);

            _service.GetSpecificRole(role.Id);
        }

        [TestMethod]
        public void GetAllRoleTest()
        {
            List<Role> dummyRole = GenerateDummyRole();
            _repoMock.Setup(repo => repo.GetAllByExpression(It.IsAny<Expression<Func<Role, bool>>>())).Returns(dummyRole);
            RoleSearchCriteria searchCriteria = new RoleSearchCriteria();

            var retrievedRole = _service.GetAllRole(searchCriteria);

            CollectionAssert.AreEqual(dummyRole, retrievedRole);
        }

        [TestMethod]
        public void UpdateRoleTest()
        {
            var role = CreateRole(1, "administrator");
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Role, bool>>>())).Returns(role);
            Role updateData = CreateRole(role.Id, "owner");

            _repoMock.Setup(repo => repo.UpdateOne(role));
            _repoMock.Setup(repo => repo.Save());

            Role updatedRole = _service.UpdateRole(role.Id, updateData);

            Assert.AreEqual(updatedRole, role);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteNotExistingRoleTest()
        {
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Role, bool>>>())).Returns((Role)null);

            _service.DeleteRole(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteRoleTest()
        {
            var role = CreateRole(1, "administrator");
            _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Role, bool>>>())).Returns(role).Returns((Role)null);
            _repoMock.Setup(repo => repo.DeleteOne(role));
            _repoMock.Setup(repo => repo.Save());

            _service.DeleteRole(role.Id);

            _service.GetSpecificRole(role.Id);
        }

        [TestMethod]
        public void CreateRoleTest()
        {
            var role = CreateRole(1, "administrator");
            _repoMock.Setup(repo => repo.InsertOne(role));
            _repoMock.Setup(repo => repo.Save());

            Role newRole = _service.CreateRole(role);

            Assert.AreEqual(newRole, role);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void CreateInvalidRoleTest()
        {
            var role = CreateRole(1, "partner");

            _service.CreateRole(role);
        }

        private List<Role> GenerateDummyRole() => new List<Role>()
        {
            new Role() { Id= 1, Permission = "administrator"},
            new Role() { Id= 2,  Permission = "owner"}
        };

        private Role CreateRole(int id, string permission)
        {
            return new Role
            {
                Id = id,
               Permission = permission
            };
        }
    }
}
