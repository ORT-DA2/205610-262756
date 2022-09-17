using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class OwnerDetailModel
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime RegisterDate { get; set; }
        public Invitation Invitation { get; set; }

        public OwnerDetailModel(Owner owner)
        {
            this.Email = owner.Email;
            this.Address = owner.Address;
            this.RegisterDate = owner.RegisterDate;
            this.Invitation = owner.Invitation;
        }
    }
}
