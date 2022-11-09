using StartUp.Domain;

namespace StartUp.Models.Models.Out
{
    public class InvitationDetailModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Rol { get; set; }
        public int Code { get; set; }
        public string State { get; set; }
        public Pharmacy Pharmacy { get; set; }

        public InvitationDetailModel(Invitation invitation)
        {
            this.Id = invitation.Id;
            this.Rol = invitation.Rol;
            this.UserName = invitation.UserName;
            this.Code = invitation.Code;
            this.Pharmacy = invitation.Pharmacy;
            this.State = invitation.State;
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
