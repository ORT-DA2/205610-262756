using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StartUp.BusinessLogic;

namespace StartUp.DomainTest
{
    [TestClass]
    public class InvitationTest
    {
        [TestMethod]
        public void ValidInvitationTest()
        {
            Invitation inv = new Invitation
            {
                Code = 000001,
                Id = 1,
                Rol = "Admin",
                UserName = "Julia"
            };

            inv.isValidInvitation();
            
            Assert.IsNotNull(inv);
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void InvalidRolInvitationTest()
        {
            Invitation inv = new Invitation
            {
                Code = 000000,
                Id = 1,
                Rol = "",
                UserName = "Julia"
            };

            inv.isValidInvitation();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void InvalidRolSpacesInvitationTest()
        {
            Invitation inv = new Invitation
            {
                Code = 000000,
                Id = 1,
                Rol = "     ",
                UserName = "Julia"
            };

            inv.isValidInvitation();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void InvalidEmptyStringUserInvitationTest()
        {
            Invitation inv = new Invitation
            {
                Code = 000000,
                Id = 1,
                Rol = "Admin",
                UserName = ""
            };

            inv.isValidInvitation();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void InvalidSpacesUserInvitationTest()
        {
            Invitation inv = new Invitation
            {
                Code = 000000,
                Id = 1,
                Rol = "Admin",
                UserName = "     "
            };

            inv.isValidInvitation();
        }
    }
}
