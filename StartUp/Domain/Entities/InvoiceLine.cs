using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class InvoiceLine
    {
        public int Id { get; set; }
        public Medicine Medicine { get; set; }
        public int Amount { get; set; }

        public InvoiceLine() { }
        public void isValidInvoiceLine()
        {
            if (Medicine == null || string.IsNullOrEmpty(Amount.ToString()))
                throw new InputException("Invoiceline empty");
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((InvoiceLine)obj);
        }

        protected bool Equals(InvoiceLine other)
        {
            return Id == other?.Id;
        }

    }
}
