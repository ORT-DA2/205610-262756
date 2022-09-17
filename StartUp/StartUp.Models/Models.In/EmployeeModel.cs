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
        public DateTime RegisterDate { get; set; }
        public Invitation Invitation { get; set; }
        public Pharmacy Pharmacy { get; set; }

        public Employee ToEntity()
        {
            return new Employee()
            {
                Email = this.Email,
                Password = this.Password,
                Address = this.Address,
                RegisterDate = this.RegisterDate,
                Invitation = this.Invitation,
                Pharmacy = this.Pharmacy
            };
        }
    }
}
