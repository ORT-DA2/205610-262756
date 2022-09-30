using Domain;
using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace StartUp.Domain
{
    public class Administrator : User
    {
        public Administrator() { }

        public override string GetType() { 
            return "administrator"; 
        }
        public void IsValidAdministrator()
        {
            if (string.IsNullOrEmpty(this.Email)
                || string.IsNullOrEmpty(this.Address)
                || this.Invitation == null
                || string.IsNullOrEmpty(this.Password))
            {
                throw new InvalidResourceException("Empty fields");
            }
            if (!EmailIsValid(this.Email))
            {
                throw new ResourceNotFoundException("Email incorrect");
            }
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

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals((Administrator)obj);
        }

        protected bool Equals(Administrator other)
        {
            return Email == other?.Email;
        }
    }
}
