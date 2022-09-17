using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class InvoiceLineBasicModel
    {
        public int Amount { get; set; }

        public InvoiceLineBasicModel(InvoiceLine line)
        {
            this.Amount = line.Amount;
        }
    }
}
