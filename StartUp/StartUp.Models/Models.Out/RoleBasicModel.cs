using StartUp.Domain.Entities;

namespace StartUp.Models.Models.Out
{
    public class RoleBasicModel
    {
        public int Id { get; set; }
        public string Permission { get; set; }

        public RoleBasicModel(Role role)
        {
            Id = role.Id;
            Permission = role.Permission;
        }

        public override bool Equals(object? obj)
        {
            if (obj is RoleBasicModel)
            {
                var otherInvitation = obj as RoleBasicModel;

                return Id == otherInvitation.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
