﻿using Domain;
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
        public void isValidAdministrator()
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
    }
}
