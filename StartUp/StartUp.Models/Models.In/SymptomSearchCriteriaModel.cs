using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.Models.Models.In
{
    public class SymptomSearchCriteriaModel
    {
        public string? SymptomDescription { get; set; }

        public SymptomSearchCriteria ToEntity()
        {
            return new SymptomSearchCriteria()
            {
                SymptomDescription = this.SymptomDescription
            };
        }
    }
}
