using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.Domain.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        public Session() { }
        public void IsValidSession()
        {
            Validator validator = new Validator();
            validator.ValidateString(Username, "Username empty");
            validator.ValidateString(Password, "Password empty");
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Session)obj);
        }

        protected bool Equals(Session other)
        {
            return Username == other?.Username;
        }
    }
}
