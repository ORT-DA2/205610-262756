﻿

using StartUp.Exceptions;

namespace StartUp.Domain
{
    public class InvoiceLine
    {
        public int Id { get; set; }
        public Medicine Medicine { get; set; }
        public int Amount { get; set; }
        public string State { get; set; }
        public int PharmacyId { get; set; }

        public InvoiceLine() { }
        public void IsValidInvoiceLine()
        {
            Validator validator = new Validator();
            validator.ValidateAmount(Amount, 1, "The Amount cant be less than 1");
            if(Medicine == null)
            {
                throw new InputException("The medicine cant be null");
            }
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
