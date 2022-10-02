using Domain;
using StartUp.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.BusinessLogic
{
    public class RolServide : IRolServide
    {
        public User user { get; set; }

        public User GetUser()
        {
            return user;
        }
    }
}
