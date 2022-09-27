using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class OwnerBasicModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public OwnerBasicModel(Owner owner)
        {
            Id = owner.Id;
            this.Email = owner.Email;
            this.Address = owner.Address;
        }

        public override bool Equals(object? obj)
        {
            if (obj is OwnerBasicModel)
            {
                var otherOwner = obj as OwnerBasicModel;

                return Id == otherOwner.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
