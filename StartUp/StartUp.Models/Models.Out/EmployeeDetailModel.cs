using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class EmployeeDetailModel
    {
        public string Email { get; set; }
        public string Address { get; set; }

        public EmployeeDetailModel(Employee employee)
        {
            this.Address = employee.Address;
            this.Email = employee.Email;
        }
    }
}
