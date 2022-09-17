using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain.SearchCriterias
{
    public class InvoiceLineSearchCriteria
    {
        public Medicine? Medicine { get; set; }
        public int? Amount { get; set; }
    }
}
