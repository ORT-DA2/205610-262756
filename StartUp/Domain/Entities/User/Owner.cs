using Domain;
using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class Owner : User
    {
        public Pharmacy Pharmacy { get; set; }

        public Owner() { }

        public override string GetType()
        {
            return "owner";
        }
        public void isValidOwner()
        {
            if (Pharmacy == null || string.IsNullOrEmpty(this.Email)
                || string.IsNullOrEmpty(this.Address)
                || this.Invitation == null
                || string.IsNullOrEmpty(this.Password))
                throw new InputException("Enter a pharmacy");
        }
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals((Owner)obj);
        }

        protected bool Equals(Owner other)
        {
            return Id == other?.Id;
        }
    }
}
