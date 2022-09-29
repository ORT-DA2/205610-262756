using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.BusinessLogic;
using StartUp.IDataAccess;
using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;

namespace StartUp.BusinessLogicTest
{
    [TestClass]
    public class InvitationServiceTest
    {
        private Mock<IRepository<Invitation>> _repoMock;
        private InvitationService _service;
            
        [TestInitialize]
        public void SetUp()
        {
            _repoMock = new Mock<IRepository<Invitation>>(MockBehavior.Strict);
            _service = new InvitationService(_repoMock.Object);
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            _repoMock.VerifyAll();
        }
        
        [TestMethod]
        public void GetSpecificInvitationTest()
        {
            var invitation = CreateInvitation();
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Invitation, bool>>>())).Returns(invitation);

            var retrievedInvitation = _service.GetSpecificInvitation(invitation.Id);
            
            Assert.AreEqual(invitation.Id, retrievedInvitation.Id);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetSpecificNullInvitationTest()
        {
            var invitation = CreateInvitation();
            
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Invitation, bool>>>())).Returns((Invitation)null);

            var retrievedInvitation = _service.GetSpecificInvitation(invitation.Id);
        }
        
        [TestMethod]
        public void GetAllInvitationsTest()
        {
            List<Invitation> dummyInvitations = GenerateDummyInvitation();
            _repoMock.Setup(repo => repo.GetAllByExpression(It.IsAny<Expression<Func<Invitation, bool>>>())).Returns(dummyInvitations);
            InvitationSearchCriteria searchCriteria = new InvitationSearchCriteria();

            var retrievedInvitations = _service.GetAllInvitation(searchCriteria);

            CollectionAssert.AreEqual(dummyInvitations, retrievedInvitations);
        }
        
        [TestMethod]
        public void UpdateInvitationTest()
        {
            var invitation = CreateInvitation();
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Invitation, bool>>>())).Returns(invitation);
            Invitation updateData = new Invitation
            {
                Id = invitation.Id,
                Rol = "Employee",
                UserName = "Luis",
                Code = 400000,
            };
            _repoMock.Setup(repo => repo.UpdateOne(invitation));
            _repoMock.Setup(repo => repo.Save());
            
            Invitation updatedInvitation = _service.UpdateInvitation(invitation.Id, updateData);

            Assert.AreEqual(updatedInvitation, invitation);
        }
        
        /*[TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteInvitationTest()
        {
            var invitation = CreateInvitation();
            _repoMock.Setup(repo => repo.InsertOne(invitation));
            _repoMock.Setup(repo => repo.DeleteOne(invitation));
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Invitation, bool>>>())).Returns((Invitation)null);
            _repoMock.Setup(repo => repo.Save());
            
            _service.DeleteInvitation(invitation.Id);
            Invitation retrivedInvi = _service.GetSpecificInvitation(invitation.Id);
        }*/

        /*[TestMethod]
        public void CreateInvitationTest()
        {
            Invitation dummyInvitation = CreateInvitation();
            _repoMock.Setup(repo => repo.InsertOne(It.IsAny<Invitation>())).Equals(dummyInvitation);
            _repoMock.Setup(repo => repo.Save());
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Invitation, bool>>>())).Returns(dummyInvitation);
                        
            InvitationService _service = new InvitationService(_repoMock.Object);

            Invitation newInvitation = _service.CreateInvitation(dummyInvitation);
            dummyInvitation.Code = newInvitation.Code;

            Assert.AreEqual(newInvitation, dummyInvitation);
        }*/
        
        private List<Invitation> GenerateDummyInvitation() => new List<Invitation>()
        {
            new Invitation() { Id = 2, UserName = "Julia", Rol = "Administrator" },
            new Invitation() { UserName = "leticia", Rol = "Employee" }
        };

        private Invitation CreateInvitation()
        {
            return new Invitation()
            {
                Id = 1,
                Rol = "Admin",
                UserName = "Julia",
                Code = 100000
            };
        }
    }
}
