using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class RequestSearchCriteriaModels
    {
        public List<Petition>? Petitions { get; set; }
        
        //preguntar
        public bool State { get; set; }

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
