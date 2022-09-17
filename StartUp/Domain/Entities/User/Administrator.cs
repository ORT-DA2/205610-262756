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
        public bool ValidOrFail()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Address))
            {
                throw new InvalidResourceException("Email or password or address empty");
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
