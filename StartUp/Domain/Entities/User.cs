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
        public string Token { get; set; }

        public User() { }

        public void IsValidUser()
        {
            Validator validator = new Validator();
            validator.ValidateString(Email, "Email empty");
            validator.ValidateString(Address, "Address empty");
            validator.ValidatePasswordValid(Password, "Password invalid", 7);
            validator.ValidateString(RegisterDate.ToString(), "Register date empty");
            validator.ValidateInvitationNotNull(Invitation, "Invitation empty");
            validator.ValidateRoleIsNotNull(Roles, "Rol empty");
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

        public void VerifyInvitationStateIsAvailable()
        {
            if(this.Invitation == null)
            {
                throw new InputException("It is not possible to create a user who does not have an invitation created");
            }
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

