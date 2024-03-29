﻿

namespace StartUp.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Permission { get; set; }

        public void IsValidRole()
        {
            Validator validator = new Validator();
            string rolesValid = "administrator, owner, employee";

            validator.ValidateString(Permission, "The user must have at least one role");
            validator.ValidateContainsRolesCorrect(rolesValid, Permission, "You are trying to enter a role that does not exist");
        }
    }
}
