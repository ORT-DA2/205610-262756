using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class SaleSearchCriteriaModel
    {
        public List<InvoiceLine>? Medicines { get; set; }
        public SaleSearchCriteria ToEntity()
        {
            return new SaleSearchCriteria()
            {
                Medicines = this.Medicines
            };
        }
    }
}
