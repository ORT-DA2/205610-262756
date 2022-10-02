

namespace StartUp.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Permission { get; set; }


        Validator validator = new Validator();

        public void IsValidRole()
        {
            string rolesValid = "administrator, owner, employee";

            validator.ValidateString(Permission, "The user must have at least one role");
            validator.ValidateContains(rolesValid, Permission, "You are trying to enter a role that does not exist");
        }
    }
}
