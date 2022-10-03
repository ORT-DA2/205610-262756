using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.BusinessLogic;
using StartUp.Domain;
using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.IDataAccess;

namespace StartUp.BusinessLogicTest;

[TestClass]
public class TokenAccessServiceTest
{
    private Mock<IRepository<TokenAccess>> _repoMock;
    private TokenAccessService _service;
    private User user;
    private Invitation invitation;

    [TestInitialize]
    public void SetUp()
    {
        _repoMock = new Mock<IRepository<TokenAccess>>(MockBehavior.Strict);
        _service = new TokenAccessService(_repoMock.Object);
        user = new User();
        invitation = new Invitation();
        user.Invitation = invitation;
        invitation.UserName = "usernameTest";
    }

    [TestCleanup]
    public void Cleanup()
    {
        _repoMock.VerifyAll();
    }

    [TestMethod]
    public void GetSpecificTokenAccessTest()
    {
        var token = CreateTokenAccess(1, new Guid(), user);
        _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<TokenAccess, bool>>>())).Returns(token);

        var retrievedTokenAccess = _service.GetSpecificTokenAccess(token.Id);

        Assert.AreEqual(token.Id, retrievedTokenAccess.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(ResourceNotFoundException))]
    public void GetSpecificNullTokenAccessTest()
    {
        var token = CreateTokenAccess(1, new Guid(), user);

        _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<TokenAccess, bool>>>())).Returns((TokenAccess)null);

        _service.GetSpecificTokenAccess(token.Id);
    }

    [TestMethod]
    public void GetAllTokenAccesssTest()
    {
        List<TokenAccess> dummyTokenAccess = GenerateDummyTokenAccess();
        _repoMock.Setup(repo => repo.GetAllByExpression(It.IsAny<Expression<Func<TokenAccess, bool>>>())).Returns(dummyTokenAccess);

        var retrievedTokenAccesss = _service.GetAllTokenAccess();

        CollectionAssert.AreEqual(dummyTokenAccess, retrievedTokenAccesss);
    }

    [TestMethod]
    public void UpdateTokenAccessTest()
    {
        var token = CreateTokenAccess(1, new Guid(), user);
        _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<TokenAccess, bool>>>())).Returns(token);
        TokenAccess updateData = new TokenAccess()
        {
            Id = token.Id,
            Token = new Guid(),
            User = new User()
        };
        _repoMock.Setup(repo => repo.UpdateOne(token));
        _repoMock.Setup(repo => repo.Save());

        Session session = new Session();
        session.Username = user.Invitation.UserName;
        session.Password = user.Password;

        TokenAccess updatedTokenAccess = _service.UpdateTokenAccess(session, updateData);

        Assert.AreEqual(updatedTokenAccess, token);
    }

    [TestMethod]
    [ExpectedException(typeof(ResourceNotFoundException))]
    public void DeleteNotExistingTokenAccessTest()
    {
        _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<TokenAccess, bool>>>())).Returns((TokenAccess)null);

        Session session = new Session();
        _service.DeleteTokenAccess(session);
    }

    [TestMethod]
    [ExpectedException(typeof(ResourceNotFoundException))]
    public void DeleteTokenAccessTest()
    {
        var token = CreateTokenAccess(1, new Guid(), user);
        _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<TokenAccess, bool>>>())).Returns(token).Returns((TokenAccess)null);
        _repoMock.Setup(repo => repo.DeleteOne(token));
        _repoMock.Setup(repo => repo.Save());

        Session session = new Session();
        session.Username = user.Invitation.UserName;
        session.Password = user.Password;   

        _service.DeleteTokenAccess(session);

        _service.GetSpecificTokenAccess(token.Id);
    }

    [TestMethod]
    public void CreateTokenAccessTest()
    {
        var token = CreateTokenAccess(1, new Guid(), user);
        _repoMock.Setup(repo => repo.InsertOne(token));
        _repoMock.Setup(repo => repo.Save());
        _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<TokenAccess, bool>>>())).Returns((TokenAccess)null);

        TokenAccess newTokenAccess = _service.CreateTokenAccess(user);

        Assert.AreEqual(newTokenAccess, token);
    }

    [TestMethod]
    [ExpectedException(typeof(Exceptions.InputException))]
    public void CreateExistingTokenAccessTest()
    {
        var token = CreateTokenAccess(1, new Guid(), user);
        _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<TokenAccess, bool>>>()))
            .Returns(token);

        _service.CreateTokenAccess(user);
    }

    private List<TokenAccess> GenerateDummyTokenAccess() => new List<TokenAccess>()
        {
            new TokenAccess() { Id = 1, Token = new Guid(), User = user},
            new TokenAccess() { Id = 2, Token = new Guid(), User = user}
        };

    private TokenAccess CreateTokenAccess(int tokenId, Guid token, User user)
    {
        return new TokenAccess
        {
            Id = tokenId,
            Token = token,
            User = user,
        };
    }
}