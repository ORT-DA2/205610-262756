using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class OwnerDetailModel
    {
        public int Id { get; set; }
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


            public override bool Equals(object? obj)
        {
            if (obj is OwnerDetailModel)
            {
                var otherOwner = obj as OwnerDetailModel;

                return Id == otherOwner.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
