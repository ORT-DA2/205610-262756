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
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Rol)
                || string.IsNullOrEmpty(Code.ToString()))
            {
                throw new InputException("Enter a Invitation");
            }
        }
    }
}
