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

        public Invitation() { }
        
        public void isValidInvitation()
        {
            Validator validator = new Validator();
            validator.ValidateString(UserName, "User name can not be empty or all spaces");
            validator.ValidateString(Rol, "User rol can not be empty or all spaces");
        }
    }
}
