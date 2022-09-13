using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class EmployeeModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }

        public Employee ToEntity()
        {
            return new Employee()
            {
                Email = this.Email,
                Password = this.Password,
                Address = this.Address
            };
        }
    }
}
