using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class InvitationDetailModel
    {
        public string UserName { get; set; }
        public string Rol { get; set; }

        public InvitationDetailModel(Invitation invitation)
        {
            this.Rol = invitation.Rol;
            this.UserName = invitation.UserName;
        }
    }
}
