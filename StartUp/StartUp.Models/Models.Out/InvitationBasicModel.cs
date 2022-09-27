using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class InvitationBasicModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Rol { get; set; }

        public InvitationBasicModel(Invitation invitation)
        {
            this.Id = invitation.Id;
            this.Rol = invitation.Rol;
            this.UserName = invitation.UserName;
        }

        public override bool Equals(object? obj)
        {
            if (obj is InvitationBasicModel)
            {
                var otherInvitation = obj as InvitationBasicModel;

                return Id == otherInvitation.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
