
using System.Collections.Generic;

namespace StartUp.Domain.SearchCriterias
{
    public class RequestSearchCriteria
    {
        public List<Petition>? Petitions { get; set; }
        public string? State { get; set; }
    }
}
