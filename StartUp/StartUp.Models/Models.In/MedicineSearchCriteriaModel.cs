using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class MedicineSearchCriteriaModel
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Presentation { get; set; }
        public int? Amount { get; set; }
        public string? Measure { get; set; }
        public int? Price { get; set; }
        public int? Stock { get; set; }
        public bool? Prescription { get; set; }

        public MedicineSearchCriteria ToEntity()
        {
            return new MedicineSearchCriteria()
            {
                Code = this.Code,
                Name = this.Name,
                Presentation = this.Presentation,
                Amount = this.Amount,
                Measure = this.Measure,
                Price = this.Price,
                Stock = this.Stock,
                Prescription = this.Prescription
            };
        }
    }
}
