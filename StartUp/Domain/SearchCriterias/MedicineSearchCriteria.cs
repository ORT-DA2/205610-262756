using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain.SearchCriterias
{
    public class MedicineSearchCriteria
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Presentation { get; set; }
        public int? Amount { get; set; }
        public string? Measure { get; set; }
        public int? Price { get; set; }
        public int? Stock { get; set; }
        public bool? Prescription { get; set; }
        public List<string>? Symptoms { get; set; }
    }
}
