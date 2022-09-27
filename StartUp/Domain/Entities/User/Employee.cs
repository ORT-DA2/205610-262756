using Domain;
using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class Employee : User
    {
        public Pharmacy Pharmacy { get; set; }

        public void isValidEmployee()
        {
            if (Pharmacy == null || string.IsNullOrEmpty(this.Email) 
                || string.IsNullOrEmpty(this.Address) 
                || this.Invitation == null 
                || string.IsNullOrEmpty(this.Password)
                || RegisterDate == null )
                throw new InputException("empty");
        }
    }
}
