using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class Owner : User
    {
        private Pharmacy Pharmacy { get; set; }
    }
}
