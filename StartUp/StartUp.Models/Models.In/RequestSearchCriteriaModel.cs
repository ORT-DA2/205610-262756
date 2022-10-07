using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System.Collections.Generic;

namespace StartUp.Models.Models.In
{
    public class RequestSearchCriteriaModel
    {
        public List<Petition>? Petitions { get; set; }
        public string? State { get; set; }

        public RequestSearchCriteria ToEntity()
        {
            return new RequestSearchCriteria()
            {
                State = this.State,
                Petitions = Petitions
            };
        }
    }
}
