using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace StartUp.Domain.Entities
{
    public enum Rol
    {
        Administrator,
        Owner,
        Employee
    }
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public List<string> Rol { get; set; }
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

        public void VerifyInvitationStateIsAvailable()
        {
            if (!Invitation.State.ToString().ToLower().Contains("available"))
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
            return Email == other?.Email;
        }
    }
}

