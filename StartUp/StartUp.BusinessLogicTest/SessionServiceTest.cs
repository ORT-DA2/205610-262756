﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.BusinessLogic;
using StartUp.Domain;
using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.BusinessLogicTest
{
    [TestClass]
    public class SessionServiceTest
    {
        private Mock<IDataAccess.IRepository<Session>> _repoSessionMock;
        private Mock<IDataAccess.IRepository<User>> _repoUserMock;
        private Mock<IDataAccess.IRepository<TokenAccess>> _repoTokenMock;
        private SessionService _service;
        private string userName;
        private string pass;

        [TestInitialize]
        public void SetUp()
        {
            _repoSessionMock = new Mock<IDataAccess.IRepository<Session>>(MockBehavior.Strict);
            _repoUserMock = new Mock<IDataAccess.IRepository<User>>(MockBehavior.Strict);
            _repoTokenMock = new Mock<IDataAccess.IRepository<TokenAccess>>(MockBehavior.Strict);
            _service = new SessionService(_repoSessionMock.Object, _repoUserMock.Object, _repoTokenMock.Object);
            userName = "Ana Paula";
            pass = "123456";
        }

        [TestCleanup]
        public void Cleanup()
        {
            _repoSessionMock.VerifyAll();
            _repoUserMock.VerifyAll();
            _repoTokenMock.VerifyAll();
        }

        [TestMethod]
        public void GetSpecificSessionTest()
        {
            var session = CreateSession(1, userName, pass);
            _repoSessionMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Session, bool>>>())).Returns(session);

            var retrievedSession = _service.GetSpecificSession(session.Username);

            Assert.AreEqual(session.Id, retrievedSession.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetSpecificNullSessionTest()
        {
            var session = CreateSession(1, userName, pass);

            _repoSessionMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Session, bool>>>())).Returns((Session)null);

            _service.GetSpecificSession(session.Username);
        }

        [TestMethod]
        public void GetAllSessionTest()
        {
            List<Session> dummySession = GenerateDummySession();
            _repoSessionMock.Setup(repo => repo.GetAllByExpression(It.IsAny<Expression<Func<Session, bool>>>())).Returns(dummySession);
            SessionSearchCriteria searchCriteria = new SessionSearchCriteria();

            var retrievedSession = _service.GetAllSession(searchCriteria);

            CollectionAssert.AreEqual(dummySession, retrievedSession);
        }

        [TestMethod]
        public void UpdateSessionTest()
        {
            var session = CreateSession(1, userName, pass);
            _repoSessionMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Session, bool>>>())).Returns(session);
            Session updateData = CreateSession(session.Id, userName, pass);

            _repoSessionMock.Setup(repo => repo.UpdateOne(session));
            _repoSessionMock.Setup(repo => repo.Save());

            Session updatedSession = _service.UpdateSession(session.Username, updateData);

            Assert.AreEqual(updatedSession, session);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteNotExistingSessionTest()
        {
            _repoSessionMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Session, bool>>>())).Returns((Session)null);

            _service.DeleteSession("Ana Paula");
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteSessionTest()
        {
            var session = CreateSession(1, userName, pass);
            _repoSessionMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Session, bool>>>())).Returns(session).Returns((Session)null);
            _repoSessionMock.Setup(repo => repo.DeleteOne(session));
            _repoSessionMock.Setup(repo => repo.Save());

            _service.DeleteSession(session.Username);

            _service.GetSpecificSession(session.Id);
        }

        [TestMethod]
        public void CreateSessionTest()
        {
            var session = CreateSession(1, userName, pass);
            _repoSessionMock.Setup(repo => repo.InsertOne(session));
            _repoSessionMock.Setup(repo => repo.Save());

            Session newSession = _service.CreateOrRetrieveSession(new SessionModel(session));

            Assert.AreEqual(newSession, session);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void CreateInvalidSessionTest()
        {
            var session = CreateSession(1, userName, pass);

            _service.CreateOrRetrieveSession(new SessionModel(session));
        }


        private List<Session> GenerateDummySession() => new List<Session>()
        {
            new Session() { Id= 1, Username="Ana", Password="password123"},
            new Session() { Id= 2, Username="Paula", Password="password456"}
        };

        private Session CreateSession(int id, string username, string password)
        {
            return new Session
            {
                Id = id,
                Username = username,
                Password = password,
            };
        }
    }
}
