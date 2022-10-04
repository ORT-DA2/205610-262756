using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class PetitionSearchCriteriaModel
    {
        public string? MedicineCode { get; set; }

        public PetitionSearchCriteria ToEntity()
        {
            return new PetitionSearchCriteria()
            {
                MedicineCode = this.MedicineCode
            };
        }
    }
}
