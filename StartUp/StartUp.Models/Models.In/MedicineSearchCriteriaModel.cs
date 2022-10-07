using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class MedicineSearchCriteriaModel
    {
        public string? Name { get; set; }
        public int? Stock { get; set; }

        public MedicineSearchCriteria ToEntity()
        {
            return new MedicineSearchCriteria()
            {
                Name = this.Name,
                Stock = this.Stock,
            };
        }
    }
}
