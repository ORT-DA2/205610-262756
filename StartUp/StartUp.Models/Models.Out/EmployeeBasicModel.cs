
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class EmployeeBasicModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public EmployeeBasicModel(Employee employee)
        {
            this.Address = employee.Address;
            this.Email = employee.Email;
            this.Id = employee.Id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is EmployeeBasicModel)
            {
                var otherEmployee = obj as EmployeeBasicModel;

                return Id == otherEmployee.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
