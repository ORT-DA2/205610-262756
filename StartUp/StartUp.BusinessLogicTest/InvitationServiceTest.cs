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
        private Mock<IDataAccess.IRepository<Pharmacy>> _repoPharmacyMock;
        private InvitationService _service;
        private Mock<IRepository<Pharmacy>> _pharmacyRepoMock;
            
        [TestInitialize]
        public void SetUp()
        {
            _repoMock = new Mock<IRepository<Invitation>>(MockBehavior.Strict);
            _pharmacyRepoMock = new Mock<IRepository<Pharmacy>>(MockBehavior.Strict);
            _service = new InvitationService(_repoMock.Object, _pharmacyRepoMock.Object);
        }
        
        [TestCleanup]
        public void Cleanup()
        {
            _repoMock.VerifyAll();
        }
        
        [TestMethod]
        public void GetSpecificInvitationTest()
        {
            var invitation = CreateInvitation("julia");
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Invitation, bool>>>())).Returns(invitation);

            var retrievedInvitation = _service.GetSpecificInvitation(invitation.Id);
            
            Assert.AreEqual(invitation.Id, retrievedInvitation.Id);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetSpecificNullInvitationTest()
        {
            var invitation = CreateInvitation("Jualia");
            
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Invitation, bool>>>())).Returns((Invitation)null);

            _service.GetSpecificInvitation(invitation.Id);
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
            var invitation = CreateInvitation("Julia");
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
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteNotExistingInvitationTest()
        {
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Invitation, bool>>>())).Returns((Invitation)null);
            
            _service.DeleteInvitation(1);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void DeleteInvitationTest()
        {
            var invitation = CreateInvitation("Julia");
            _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Invitation, bool>>>())).Returns(invitation).Returns((Invitation)null);
            _repoMock.Setup(repo => repo.DeleteOne(invitation));
            _repoMock.Setup(repo => repo.Save());
            
            _service.DeleteInvitation(invitation.Id);

            _service.GetSpecificInvitation(invitation.Id);
        }

        [TestMethod]
        public void CreateInvitationTest()
        {
            Invitation dummyInvitation = CreateInvitation("Julia");
            _repoMock.Setup(repo => repo.InsertOne(dummyInvitation));
            _repoMock.Setup(repo => repo.Save());
            _repoMock.Setup(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Invitation, bool>>>())).Returns((Invitation)null);
            _pharmacyRepoMock.Setup(fRepo => fRepo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>())).Returns(dummyInvitation.Pharmacy);

            Invitation newInvitation = _service.CreateInvitation(dummyInvitation);
            dummyInvitation.Code = newInvitation.Code;

            Assert.AreEqual(newInvitation, dummyInvitation);
        }
        
        [TestMethod]
        public void CreateExistingCodeInvitationTest()
        {
            Invitation dummyInvitation = CreateInvitation("Julia");
            _repoMock.Setup(repo => repo.InsertOne(dummyInvitation));
            _repoMock.Setup(repo => repo.Save());
            _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Invitation, bool>>>()))
                .Returns((Invitation) null)
                .Returns(dummyInvitation)
                .Returns((Invitation) null);
            _pharmacyRepoMock.Setup(fRepo => fRepo.GetOneByExpression(It.IsAny<Expression<Func<Pharmacy, bool>>>()))
                .Returns(dummyInvitation.Pharmacy);
            
            Invitation newInvitation = _service.CreateInvitation(dummyInvitation);
            dummyInvitation.Code = newInvitation.Code;

            Assert.AreEqual(newInvitation, dummyInvitation);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InputException))]
        public void CreateExistingUserNameInvitationTest()
        {
            Invitation dummyInvitation = CreateInvitation("Julia");
            _repoMock.SetupSequence(repo => repo.GetOneByExpression(It.IsAny<Expression<Func<Invitation, bool>>>()))
                .Returns(dummyInvitation);
            
            _service.CreateInvitation(dummyInvitation);
        }
        
        private List<Invitation> GenerateDummyInvitation() => new List<Invitation>()
        {
            new Invitation() { Id = 2, UserName = "Julia", Rol = "Administrator" },
            new Invitation() { UserName = "leticia", Rol = "Employee" }
        };

        private Invitation CreateInvitation(string user)
        {
            List<Medicine> listMedicines = new List<Medicine>();
            List<Request> listRequests = new List<Request>();

            Pharmacy pharmacy = new Pharmacy
            {
                Name = "Una farmacia", 
                Address = "18 de julio",
                Stock = listMedicines,
                Requests = listRequests
            };
            
            return new Invitation()
            {
                Id = 1,
                Rol = "Admin",
                UserName = user,
                Code = 100000,
                State = "Available",
                Pharmacy = pharmacy,
            };
        }
    }
}
