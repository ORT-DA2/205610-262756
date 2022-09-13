using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class OwnerBasicModel
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public OwnerBasicModel(Owner owner)
        {
            this.Email = owner.Email;
            this.Address = owner.Address;
        }
    }
}
