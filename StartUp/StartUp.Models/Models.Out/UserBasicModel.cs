
using StartUp.Domain;
using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class UserBasicModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<string> Rol { get; set; }

        public UserBasicModel(User user)
        {
            this.Address = user.Address;
            this.Email = user.Email;
            this.Id = user.Id;
            Rol = user.Rol;
        }

        public override bool Equals(object? obj)
        {
            if (obj is UserBasicModel)
            {
                var otheruser = obj as UserBasicModel;

                return Id == otheruser.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
