using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.Models.Models.In;

namespace StartUp.WebApi.UnitTest
{
    [TestClass]
    public class InvitationControllerTest
    {
        private Mock<IInvitationService> _serviceMock;
        private InvitationController _controller;

        [TestInitialize]
        public void Setup()
        {
            _serviceMock = new Mock<IInvitationService>(MockBehavior.Strict);
            _controller = new InvitationController(_serviceMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _serviceMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingInvitationTest()
        {
            var invitation = CreateInvitation();
            var expectedInvitation = new InvitationDetailModel(invitation);
            _serviceMock.Setup(manager => manager.GetSpecificInvitation(It.IsAny<int>())).Returns(invitation);

            var response = _controller.GetInvitation(invitation.Id);
            var okResponseObject = response as OkObjectResult;

            Assert.AreEqual(expectedInvitation, okResponseObject.Value);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetNotExistingInvitationTest()
        {
            Invitation inv = CreateInvitation();
            _serviceMock.Setup(manager => manager.GetSpecificInvitation(It.IsAny<int>())).Returns((Invitation)null);

            _controller.GetInvitation(inv.Id);
        }
        
        [TestMethod]
        public void GetExistingInvitationWithModelTest()
        {
            InvitationSearchCriteriaModel invitationModel = new InvitationSearchCriteriaModel(); 
            //invitationModel.Rol = "Administrator";
            invitationModel.UserName = "apodo";
            List<Invitation> invList = new List<Invitation>();
            Invitation inv = CreateInvitation();
            invList.Add(inv);
            
            _serviceMock.Setup(manager => manager.GetAllInvitation(It.IsAny<InvitationSearchCriteria>())).Returns(invList);

            _controller.GetInvitation(invitationModel);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public void GetNotExistingInvitationWithModelTest()
        {
            InvitationSearchCriteriaModel invitationModel = new InvitationSearchCriteriaModel();
            var exception = new ResourceNotFoundException("No invitations where found");

            _serviceMock.Setup(manager => manager.GetAllInvitation(It.IsAny<InvitationSearchCriteria>())).Throws(exception);

            var response = _controller.GetInvitation(invitationModel);
        }
        
        [TestMethod]
        public void CreateInvitationTest()
        {
            InvitationModel invitationModel = CreateInvitationModel();
            Invitation invitation = CreateInvitation();
            _serviceMock.Setup(server => server.CreateInvitation(It.IsAny<Invitation>())).Returns(invitation);

            var response = _controller.CreateInvitation(invitationModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.Created, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void UpdateInvitationTest()
        {
            InvitationModel invitationModel = CreateInvitationModel();
            Invitation invitation = CreateInvitation();
            invitationModel.UserName = "Jeniffer";
            _serviceMock.Setup(server => server.UpdateInvitation(invitation.Id, invitationModel.ToEntity())).Returns(invitationModel.ToEntity);

            var response = _controller.Update(invitation.Id, invitationModel);
            var okResponseObject = response as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }
        
        [TestMethod]
        public void DeleteInvitationTest()
        {
            Invitation invitation = CreateInvitation();
            _serviceMock.Setup(server => server.DeleteInvitation(invitation.Id));

            var response = _controller.Delete(invitation.Id);
            var okResponseObject = response as OkResult;

            Assert.AreEqual((int)HttpStatusCode.OK, okResponseObject.StatusCode);
        }

        private Invitation CreateInvitation()
        {
            return new Invitation()
            {
                Id = 1,
                Code = 1236,
                Rol = "Administrator",
                UserName = "apodo"
            };
        }
        
        private InvitationModel CreateInvitationModel()
        {
            return new InvitationModel()
            {
                Rol = "Administrator",
                UserName = "apodo",
                Pharmacy = new Pharmacy(),
            };
        }
    }
}
