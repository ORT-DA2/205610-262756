using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class AdministratorBasicModel
    {
        public string Email { get; set; }
        public string Address { get; set; }

        public AdministratorBasicModel(Administrator admin)
        {
            this.Email = admin.Email;
            this.Address = admin.Address;
        }
    }
}
