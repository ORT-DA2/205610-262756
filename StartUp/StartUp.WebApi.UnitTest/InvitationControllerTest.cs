using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StartUp.Domain;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.WebApi.UnitTest
{
    [TestClass]
    public class InvitationControllerTest
    {
        private Mock<IInvitationService> _managerMock;

        [TestInitialize]
        public void Setup()
        {
            _managerMock = new Mock<IInvitationService>(MockBehavior.Strict);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _managerMock.VerifyAll();
        }

        [TestMethod]
        public void GetExistingInvitationReturnsAsExpected()
        {
            var invitation = CreateInvitation();
            var expectedInvitation = new InvitationDetailModel(invitation);
            _managerMock.Setup(manager => manager.GetSpecificInvitation(It.IsAny<int>())).Returns(invitation);
            var controller = new InvitationController(_managerMock.Object);

            var response = controller.GetInvitation(invitation.Id);
            var okResponseObject = response as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(expectedInvitation, okResponseObject.Value);
        }

        private Invitation CreateInvitation()
        {
            return new Invitation()
            {
                Code = 1236,
                Rol = "Administrator",
                UserName = "apodo"
            };
        }
    }
}
