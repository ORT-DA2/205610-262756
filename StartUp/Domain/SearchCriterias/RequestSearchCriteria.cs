using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain.SearchCriterias
{
    public class RequestSearchCriteria
    {
        public List<Petition>? Petitions { get; set; }
        public bool? State { get; set; }
    }
}
