using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class EmployeeDetailModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime RegisterDate { get; set; }
        public Invitation Invitation { get; set; }

        public EmployeeDetailModel(Employee employee)
        {
            this.Address = employee.Address;
            this.Email = employee.Email;
            this.RegisterDate = employee.RegisterDate;
            this.Invitation = employee.Invitation;
        }

        public override bool Equals(object? obj)
        {
            if (obj is EmployeeDetailModel)
            {
                var otherEmployee = obj as EmployeeDetailModel;

                return Id == otherEmployee.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
