using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class InvitationModel
    {
        public string UserName { get; set; }
        public string Rol { get; set; }
        public int Code { get; set; }

        public Invitation ToEntity()
        {
            return new Invitation()
            {
               UserName = this.UserName,
               Rol = this.Rol,
               Code = this.Code
            };
        }
    }
}
