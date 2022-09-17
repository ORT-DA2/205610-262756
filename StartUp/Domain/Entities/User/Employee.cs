using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class Employee : User
    {
        public Pharmacy Pharmacy { get; set; }
    }
}
