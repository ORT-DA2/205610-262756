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
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Address))
            {
                throw new InputException("Name or address empty");
            }
            if (Name.Length > 50)
            {
                throw new InputException("the name of the pharmacy must not exceed 50 characters");
            }
            if (Stock == null)
            {
                throw new InputException("The list of medicines must be created");
            }
            if(Requests == null)
            {
                throw new InputException("The list of request must be created");
            }
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
