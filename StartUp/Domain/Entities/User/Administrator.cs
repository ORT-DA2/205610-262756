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
        public bool isValidAdministrator()
        {
            if (string.IsNullOrEmpty(this.Email)
                || string.IsNullOrEmpty(this.Address)
                || this.Invitation == null
                || string.IsNullOrEmpty(this.Password)
                || RegisterDate == null)
            {
                throw new InvalidResourceException("Empty fields");
            }
            return true;
        }

        public bool EmailIsValid(string emailAddress)
        {/*
            try
            {
                MailAddress m = new MailAddress(emailAddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }*/
            return true;
        }
    }
}
