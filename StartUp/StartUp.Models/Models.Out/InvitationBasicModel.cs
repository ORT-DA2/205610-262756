using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class InvitationBasicModel
    {
        public string UserName { get; set; }
        public string Rol { get; set; }

        public InvitationBasicModel(Invitation invitation)
        {
            this.Rol = invitation.Rol;
            this.UserName = invitation.UserName;
        }
    }
}
