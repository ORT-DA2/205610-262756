using StartUp.Exceptions;
using System;
using System.Net.Mail;

namespace StartUp.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public Role Roles { get; set; }
        public DateTime RegisterDate { get; set; }
        public Invitation Invitation { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public string Token { get; set; }

        public User() { }

        public void IsValidUser()
        {
            Validator validator = new Validator();
            validator.ValidateString(Email, "Email empty");
            validator.ValidateString(Address, "Address empty");
            validator.ValidatePasswordValid(Password, "Password invalid", 7);
            validator.ValidateString(RegisterDate.ToString(), "Register date empty");
            EmailIsValid(Email);
            
            if(Roles == null)
            {
                throw new InputException("Roles empty");
            }
            if (Invitation == null)
            {
                throw new InputException("Invitation empty");
            }
        }

        public void EmailIsValid(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);
            }
            catch (FormatException)
            {
                throw new InputException("Wrong email format");
            }
        }

        public bool HasPermissions(string[] permissions)
        {
            bool hasPermission = false;
            foreach (string permission in permissions)
            {
                if(permission.Contains(this.Roles.Permission))
                {
                    hasPermission = true;
                }
            }

            return hasPermission;
        }

        public void VerifyInvitationState()
        {
            if (Invitation.State.ToLower() != "available")
            {
                throw new InputException("The invitation has already been used");
            }
        }

        public void VerifyInvitationExist()
        {
            if (this.Invitation == null)
            {
                throw new InputException("It is not possible to create a user who does not have an invitation created");
            }
        }

        public void VerifyInvitationRoles()
        {
            if(this.Invitation.Rol != Roles.Permission)
            {
                throw new InputException("The user must be created with the permission that was assigned in the invitation");
            }
        }

        public void VerifyRolesAndPharmacy()
        {
           if(this.Invitation.Rol.ToLower() == "administrator" && this.Pharmacy != null || 
                this.Invitation.Rol.ToLower() != "administrator" && this.Pharmacy == null){
                throw new InputException("The role does not match the pharmacy");
            }
        }

        public void VerifyInvitationPharmacy()
        {
            if (this.Invitation.Rol != "administrator")
            {
                if (this.Invitation.Pharmacy.Name != this.Pharmacy.Name)
                {
                    throw new InputException(
                        "The user must have assigned the pharmacy that has assigned their invitation");
                }
            }
        }

        public void ChangeStatusInvitation()
        {
            Invitation.State = "Not available";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals((User)obj);
        }

        protected bool Equals(User other)
        {
            return Id == other?.Id;
        }
    }
}

