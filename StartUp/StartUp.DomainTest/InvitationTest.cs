using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;

namespace StartUp.DomainTest
{
    [TestClass]
    public class InvitationTest
    {
        private Pharmacy pharmacy1;
        
        [TestInitialize]
        public void Setup()
        {
            pharmacy1 = new Pharmacy();
        }
        
        [TestMethod]
        public void ValidInvitationTest()
        {
            Invitation inv = CreateInvitation(000000,1,"Admin","Julia", pharmacy1, "Available");

            inv.IsValidInvitation();
            
            Assert.IsNotNull(inv);
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void InvalidRolInvitationTest()
        {
            Invitation inv = CreateInvitation(000000,1,"","Julia", pharmacy1, "Available");

            inv.IsValidInvitation();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void InvalidRolSpacesInvitationTest()
        {
            Invitation inv = CreateInvitation(000000,1,"  ","Julia", pharmacy1, "Available");

            inv.IsValidInvitation();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void InvalidEmptyStringUserInvitationTest()
        {
            Invitation inv = CreateInvitation(000000,1,"Admin","", pharmacy1, "Available" );


            inv.IsValidInvitation();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void InvalidSpacesUserInvitationTest()
        {
            Invitation inv = CreateInvitation(000000,1,"Admin","    ", pharmacy1, "Available");

            inv.IsValidInvitation();
        }
        
        [TestMethod]
        public void CompareSameInvitationTest()
        {
            Invitation inv = CreateInvitation(000000,1,"Admin","julia", pharmacy1, "Available");
            Invitation inv2 = CreateInvitation(000000,1,"Admin","julia", pharmacy1, "Available");

            bool areSame = inv.Equals(inv2);
            
            Assert.IsTrue(areSame);
        }
        
        [TestMethod]
        public void CompareDifferentInvitationTest()
        {
            Invitation inv = CreateInvitation(000000,1,"Admin","julia", pharmacy1, "Available");
            Invitation inv2 = CreateInvitation(000000,1,"Admin","Juan", pharmacy1, "Available");

            bool areSame = inv.Equals(inv2);
            
            Assert.IsFalse(areSame);
        }

        private Invitation CreateInvitation(int code, int id, string rol, string userName, Pharmacy pharmacy, string state)
        {
            return new Invitation
            {
                Code = code,
                Id = id,
                Rol = rol,
                UserName = userName,
                Pharmacy = pharmacy,
                State = state,
            };
        }
    }
}
