using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StartUp.Domain
{
    public class Pharmacy
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public List<Medicine> Stock { get; set; }

        public List<Request> Requests { get; set; }


        public Pharmacy() { }

        public Pharmacy(string name, string address, List<Medicine> medicines, List<Request> requests)
        {
            this.Name = name;
            this.Address = address;
            this.Stock = medicines;
            this.Requests = requests;
        }

        public void isValidPharmacy()
        {

            Validator validator = new Validator();
            validator.ValidateString(Name, "Name can not be empty or all spaces");
            validator.ValidateString(Address, "Address can not be empty or all spaces");
            validator.ValidateLengthString(Name, "The name of the pharmacy must not exceed 50 characters", 50);
            //validator.ValidateNotNull(Stock, "The list of medicines must be created");
            //validator.ValidateNotNull(Requests, "The list of request must be created");
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
    }

}
