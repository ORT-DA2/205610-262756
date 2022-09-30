using StartUp.Domain;
using System;

namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime RegisterDate { get; set; }
        public Invitation Invitation { get; set; }

        public virtual string GetType() { return "user"; }
    }
}

