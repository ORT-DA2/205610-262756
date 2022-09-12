using StartUp.Domain;
using System;

namespace Domain
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        private DateTime RegisterDate { get; set; }
        public string Address { get; set; }
        private Invitation Invitation { get; set; }
    }
}

