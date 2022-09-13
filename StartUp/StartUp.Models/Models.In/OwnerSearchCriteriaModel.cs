using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class OwnerSearchCriteriaModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Address { get; set; }

        public OwnerSearchCriteria ToEntity()
        {
            return new OwnerSearchCriteria()
            {
                Email = this.Email,
                Password = this.Password,
                Address = this.Address
            };
        }
    }
}
