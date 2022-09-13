using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class EmployeeSearchCriteriaModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }

        public EmployeeSearchCriteria ToEntity()
        {
            return new EmployeeSearchCriteria()
            {
                Email = this.Email,
                Password = this.Password,
                Address = this.Address
            };
        }
    }
}
