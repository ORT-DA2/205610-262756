using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class AdministratorDetailModel
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime RegisterDate { get; set; }
        public Invitation Invitation { get; set; }

        public AdministratorDetailModel(Administrator admin)
        {
            this.Address = admin.Address;
            this.Email = admin.Email;
            this.RegisterDate = admin.RegisterDate;
            this.Invitation = admin.Invitation;
        }
    }
}
