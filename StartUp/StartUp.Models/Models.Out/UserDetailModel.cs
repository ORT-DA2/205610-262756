using StartUp.Domain;
using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class UserDetailModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Role Roles { get; set; }
        public DateTime RegisterDate { get; set; }
        public Invitation Invitation { get; set; }

        public UserDetailModel(User user)
        {
            this.Address = user.Address;
            this.Email = user.Email;
            this.RegisterDate = user.RegisterDate;
            this.Invitation = user.Invitation;
            this.Roles = user.Roles;
        }

        public override bool Equals(object? obj)
        {
            if (obj is UserDetailModel)
            {
                var otherUser = obj as UserDetailModel;

                return Id == otherUser.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
