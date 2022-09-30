using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class Invitation
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Rol { get; set; }
        public int Code { get; set; }
        public string State { get; set; }
        public Pharmacy Pharmacy { get; set; }

        public Invitation() { }

        public void IsValidInvitation()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Rol))
            {
                throw new InputException("Empty fields");
            }
            if(Rol.ToLower() != "administrator" && Rol.ToLower() != "owner" && Rol.ToLower() != "employee")
            {
                throw new InvalidResourceException("The user role must be administrator, owner or employee");
            }
            if(Pharmacy == null && Rol != "administrator")
            {
                throw new InvalidResourceException("Select a partner pharmacy for this type of user");
            }
        }

        public void SetCodeAndState()
        {
            Code = Int32.Parse(GenerateNewCode());
            State = "Available";
        }

        public string GenerateNewCode()
        {
            var chars = "0123456789";
            var generatedCode = new char[6];
            var random = new Random();
            for (int i = 0; i < generatedCode.Length; i++)
            {
                generatedCode[i] = chars[random.Next(chars.Length)];
            }

            return new String(generatedCode);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Invitation)obj);
        }

        protected bool Equals(Invitation other)
        {
            return UserName == other?.UserName;
        }
    }
}
