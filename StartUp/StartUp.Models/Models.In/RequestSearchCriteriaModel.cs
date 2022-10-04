using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class RequestSearchCriteriaModel
    {
        public List<Petition>? Petitions { get; set; }
        public bool? State { get; set; }

        public RequestSearchCriteria ToEntity()
        {
            return new RequestSearchCriteria()
            {
                Petitions = this.Petitions,
                State = this.State
            };
        }
    }
}
