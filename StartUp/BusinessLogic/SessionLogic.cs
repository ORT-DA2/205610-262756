﻿using StartUp.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.BusinessLogic
{
    public class SessionLogic : ISessionLogic
    {
        public bool ValidateToken()
        {
            return true;
        }
    }
}
