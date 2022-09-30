﻿using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.Models.Models.In;
public class SessionModel
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public Session ToEntity()
    {
        return new Session()
        {
            Username = UserName,
            Password = Password
        };
    }
}
