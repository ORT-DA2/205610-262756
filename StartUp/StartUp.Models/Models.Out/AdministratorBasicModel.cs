using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class AdministratorBasicModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public AdministratorBasicModel(Administrator admin)
        {
            this.Id = admin.Id;
            this.Email = admin.Email;
            this.Address = admin.Address;
        }

        public override bool Equals(object? obj)
        {
            if (obj is AdministratorBasicModel)
            {
                var otherAdmin = obj as AdministratorBasicModel;

                return Id == otherAdmin.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
