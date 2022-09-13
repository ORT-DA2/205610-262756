using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class SaleModel
    {
        public List<InvoiceLine> Medicines { get; set; }
        public Sale ToEntity()
        {
            return new Sale()
            {
               Medicines = this.Medicines
            };
        }
    }
}
