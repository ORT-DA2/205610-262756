

using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain.Entities;
using System;

namespace StartUp.DomainTest
{
    [TestClass]
    public class TokenAccessTest
    {
        private User user;

        [TestInitialize]
        public void Setup()
        {
            user = new User();
        }

        [TestMethod]
        public void NewTokenAccessTest()
        {
            TokenAccess token = CreateTokenAccess(1, new Guid(), user);

            token.IsValidTokenAccess();

            Assert.IsNotNull(token);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewTokenAccessWithNoUserTest()
        {
            TokenAccess token = CreateTokenAccess(1, new Guid(), null);

            token.IsValidTokenAccess();

            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void CompareNullTokenTest()
        {
            TokenAccess token = CreateTokenAccess(1, new Guid(), user);

            TokenAccess token2 = null;

            bool areEqual = token.Equals(token2);

            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void CompareEqualTokenTokenAccessTest()
        {
            Guid guid = Guid.NewGuid();

            TokenAccess token = CreateTokenAccess(1, guid, user);

            TokenAccess token2 = CreateTokenAccess(2, guid, user);

            bool areEqual = token.Equals(token2);

            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void CompareDifferentTokenTokenAccessTest()
        {
            Guid guid = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();

            TokenAccess token = CreateTokenAccess(1, guid, user);

            TokenAccess token2 = CreateTokenAccess(2, guid2, user);

            bool areEqual = token.Equals(token2);

            Assert.IsTrue(areEqual);
        }

        private TokenAccess CreateTokenAccess(int id, Guid token, User user)
        {
            return new TokenAccess()
            {
                Id = id,
                User = user,
                Token = token
            };
        }
    }
}
