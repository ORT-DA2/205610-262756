using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class PharmacySearchCriteriaModel
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<Medicine>? Stock { get; set; }
        public List<Request>? Requests { get; set; }
        public List<Sale>? Sales { get; set; }
        
        public List<Petition>? Petitions { get; set; }

        public PharmacySearchCriteria ToEntity()
        {
            return new PharmacySearchCriteria()
            {
                Name = this.Name,
                Address = this.Address,
                Stock = this.Stock,
                Requests = this.Requests,
                Sales = this.Sales,
                Petitions = this.Petitions
            };
        }
    }
}
