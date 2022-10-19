using StartUp.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.Domain;
using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BusinessLogic;
using StartUp.IDataAccess;

namespace StartUp.BusinessLogicTest
{
    [TestClass]
    public class UserServiceTest
    {

        private Mock<IRepository<User>> _repoMock;
        private Mock<IRepository<Role>> _roleRepoMock;
        private Mock<IRepository<Pharmacy>> _pharmacyRepoMock;
        private UserService _service;
        private Pharmacy pharmacy1;
        private Invitation invitation1;
        private Invitation invitation2;
        private Pharmacy pharmacy2;
        private Role role;

        [TestInitialize]
        public void SetUp()
        {
            _repoMock = new Mock<IRepository<User>>(MockBehavior.Strict);
            _roleRepoMock = new Mock<IRepository<Role>>(MockBehavior.Strict);
            _pharmacyRepoMock = new Mock<IRepository<Pharmacy>>(MockBehavior.Strict);
            _service = new UserService(_repoMock.Object, _roleRepoMock.Object, _pharmacyRepoMock.Object);
            pharmacy1 = new Pharmacy();
            invitation1 = new Invitation();
            pharmacy2 = new Pharmacy();
            invitation2 = new Invitation();
            role = new Role();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _repoMock.VerifyAll();
        }

        [TestMethod]
        public void GetSpecificUserTest()
        {
            var user = CreateUser(1, "paula@gmail.com", "Libertador", "asdfghjkl", invitation1, pharmacy1, DateTime.Now, role, "");
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);

            var retrievedUser = _service.GetSpecificUser(user.Id);

            Assert.AreEqual(user.Id, retrievedUser.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetSpecificNullUserTest()
        {
            var user = CreateUser(1, "paula@gmail.com", "Libertador", "asdfghjkl", invitation1, pharmacy1, DateTime.Now, role, "");

            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).Returns((User)null);

            _service.GetSpecificUser(user.Id);
        }

        [TestMethod]
        public void GetAllUserTest()
        {
            List<User> dummyUser = GenerateDummyUser();
            _repoMock.Setup(repo => repo.GetAllByExpression(It.IsAny<Expression<Func<User, bool>>>())).Returns(dummyUser);
            UserSearchCriteria searchCriteria = new UserSearchCriteria();

            var retrievedUser = _service.GetAllUser(searchCriteria);

            CollectionAssert.AreEqual(dummyUser, retrievedUser);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var user = CreateUser(1, "paula@gmail.com", "Libertador", "asdfghjkl", invitation1, pharmacy1, DateTime.Now, role, "");
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
            User updateData = CreateUser(user.Id, "Paula", "Libertador", "asdfghjkl", invitation1, pharmacy1, DateTime.Now, role, "");

            _repoMock.Setup(repo => repo.UpdateOne(user));
            _repoMock.Setup(repo => repo.Save());

            User updatedUser = _service.UpdateUser(user.Id, updateData);

            Assert.AreEqual(updatedUser, user);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteNotExistingUserTest()
        {
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).Returns((User)null);

            _service.DeleteUser(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteUserTest()
        {
            var user = CreateUser(1, "paula@gmail.com", "Libertador", "asdfghjkl", invitation1, pharmacy1, DateTime.Now, role, "");
            _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<User, bool>>>())).Returns(user).Returns((User)null);
            _repoMock.Setup(repo => repo.DeleteOne(user));
            _repoMock.Setup(repo => repo.Save());

            _service.DeleteUser(user.Id);

            _service.GetSpecificUser(user.Id);
        }

        [TestMethod]
        public void CreateUserTest()
        {
            var user = CreateUser(1, "paula@gmail.com", "Libertador", "asdfghjkl", invitation1, pharmacy1, DateTime.Now, role, "");
            _repoMock.Setup(repo => repo.InsertOne(user));
            _repoMock.Setup(repo => repo.Save());

            User newUser = _service.CreateUser(user);

            Assert.AreEqual(newUser, user);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void CreateInvalidUserTest()
        {
            var user = CreateUser(1, "paula@gmail.com", "Libertador", "asdfghjkl", null, pharmacy1, DateTime.Now, role, "");

            _service.CreateUser(user);
        }
        
        [TestMethod]
        public void SaveTokenTest()
        {
            var user = CreateUser(1, "paula@gmail.com", "Libertador", "asdfghjkl", null, pharmacy1, DateTime.Now, role, "");

            _service.SaveToken(user, "newToken");
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void SaveTokenInvalidTest()
        {
            var user = CreateUser(5, "paula@gmail.com", "Libertador", "asdfghjkl", null, pharmacy1, DateTime.Now, role, "");

            _service.SaveToken(user, "newToken");
        }

        private List<User> GenerateDummyUser() => new List<User>()
        {
            new User() { Id= 1, Email = "paula@gmail.com", Address = "Belvedere", Password = "123456",
            Invitation = invitation1, Pharmacy = pharmacy1, RegisterDate = DateTime.Now, Roles = role, Token = ""},
            new User() { Id= 2, Email = "belu@gmail.com", Address = "Centenario", Password = "123456",
            Invitation = invitation2, Pharmacy = pharmacy2, RegisterDate = DateTime.Now, Roles = role, Token = ""}
        };

        private User CreateUser(int id, string email, string address, string password, Invitation invitation,
            Pharmacy pharmacy, DateTime registerDate, Role role, string token)
        {
            return new User
            {
                Id = id,
                Email = email,
                Address = address,
                Password = password,
                Invitation = invitation,
                Pharmacy = pharmacy,
                RegisterDate = registerDate,
                Roles = role,
                Token = token,
            };
        }
    }
}
