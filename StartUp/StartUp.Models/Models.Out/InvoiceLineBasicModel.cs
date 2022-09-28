using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class InvoiceLineBasicModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        public InvoiceLineBasicModel(InvoiceLine line)
        {
            Id = line.Id;
            this.Amount = line.Amount;
        }



        public override bool Equals(object? obj)
        {
            if (obj is InvoiceLineBasicModel)
            {
                var otherInvoiceLine = obj as InvoiceLineBasicModel;

                return Id == otherInvoiceLine.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
