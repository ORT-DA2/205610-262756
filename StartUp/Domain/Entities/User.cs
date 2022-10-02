using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Validator validator = new Validator();

        public User() { }

        public void IsValidUser()
        {
            validator.ValidateString(Email, "Email empty");
            validator.ValidateString(Address, "Address empty");
            validator.ValidateString(Password, "Password empty");
            validator.ValidateString(RegisterDate.ToString(), "Register date empty");
            validator.ValidateInvitationNotNull(Invitation, "Invitation empty");
            validator.ValidateRoleIsNotNull(Roles, "Role empty");
        }

        public bool EmailIsValid(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool HasPermissions(string[] permissions)
        {
            string[] permList = this.Roles.Permission.Split(",");
            var hasPermission = permList.ToList().Any(permission => permissions.Contains(permission));

            return hasPermission;
        }

        public void VerifyInvitationStateIsAvailable()
        {
            if (Invitation.State.ToLower() != "available")
            {
                throw new InputException("The invitation has already been used");
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

