using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.Models.Models.In
{
    public class RoleModel
    {
        public string Permission { get; set; }

        public Role ToEntity()
        {
            return new Role()
            {
               Permission = Permission,
            };
        }
    }
}
