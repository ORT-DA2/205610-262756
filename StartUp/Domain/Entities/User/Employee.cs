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

        public Employee() { }
        public void isValidEmployee()
        {
            if (Pharmacy == null || string.IsNullOrEmpty(this.Email) 
                || string.IsNullOrEmpty(this.Address) 
                || this.Invitation == null 
                || string.IsNullOrEmpty(this.Password))
                throw new InputException("empty");
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Employee)obj);
        }

        protected bool Equals(Employee other)
        {
            return Id == other?.Id;
        }
    }
}
