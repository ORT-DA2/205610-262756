using StartUp.Domain;
using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace StartUp.Models.Models.In
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public Role Roles { get; set; }
        public DateTime RegisterDate { get; set; }
        public Invitation Invitation { get; set; }
        public Pharmacy Pharmacy { get; set; }

        public User ToEntity()
        {
            return new User()
            {
                Email = this.Email,
                Password = this.Password,
                Address = this.Address,
                RegisterDate = this.RegisterDate,
                Invitation = this.Invitation,
                Pharmacy = this.Pharmacy,
                Roles = this.Roles
            };
        }
    }
}
