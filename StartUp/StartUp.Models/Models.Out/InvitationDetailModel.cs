using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class InvitationDetailModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Rol { get; set; }
        public int Code { get; set; }

        public InvitationDetailModel(Invitation invitation)
        {
            this.Rol = invitation.Rol;
            this.UserName = invitation.UserName;
            this.Code = invitation.Code;
        }

        public override bool Equals(object? obj)
        {
            if (obj is InvitationDetailModel)
            {
                var otherInvitation = obj as InvitationDetailModel;

                return Id == otherInvitation.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
