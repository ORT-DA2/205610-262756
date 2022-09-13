using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain.SearchCriterias
{
    public class PharmacySearchCriteria
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<Medicine>? Stock { get; set; }
        public List<Request>? Requests { get; set; }
    }
}
