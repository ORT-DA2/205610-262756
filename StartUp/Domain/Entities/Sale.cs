using StartUp.Exceptions;
using System;
using System.Collections.Generic;

namespace StartUp.Domain
{
    public class Sale
    {
        public int Id { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }
        public int Code { get; set; }


        public Sale() { }
        public void isValidSale()
        {
            if (InvoiceLines == null)
                throw new InputException("Invoice lines empty");
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Sale)obj);
        }

        protected bool Equals(Sale other)
        {
            return Id == other?.Id;
        }

        public void ValidateState()
        {
           foreach (var line in InvoiceLines)
            {
                if(line.State != "approved" && line.State != "rejected" && line.State != "pending")
                {
                    throw new InputException("Sales can only be approved or rejected");
                }
            }
        }
    }
}
