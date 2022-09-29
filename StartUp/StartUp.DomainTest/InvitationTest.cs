using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.DomainTest
{
    [TestClass]
    public class InvitationTest
    {
        [TestMethod]
        public void ValidOrFailPassesWithValidAdministrator()
        {
            Invitation inv = new Invitation();
            inv.IsValidInvitation();
        }
    }
}
