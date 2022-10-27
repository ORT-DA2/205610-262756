using System.Collections.Generic;

namespace StartUp.Domain
{
    public class Pharmacy
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public List<Medicine> Stock { get; set; }

        public List<Request> Requests { get; set; }

        public List<Sale> Sales { get; set; }


        public Pharmacy()
        {
            Sales = new List<Sale>();
        }

        public void isValidPharmacy()
        {
            Validator validator = new Validator();
            validator.ValidateString(Name, "Name can not be empty or all spaces");
            validator.ValidateString(Address, "Address can not be empty or all spaces");
            validator.ValidateLengthString(Name, "The name of the pharmacy must not exceed 50 characters", 50);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Pharmacy)obj);
        }

        protected bool Equals(Pharmacy other)
        {
            return Name == other?.Name;
        }

        public void UpdateStock(Sale sale, InvoiceLine item)
        {
            foreach (InvoiceLine line in sale.InvoiceLines)
            {
                if (line == item)
                {
                    line.Amount = line.Amount - item.Amount;
                }
            }
        }
    }
}

