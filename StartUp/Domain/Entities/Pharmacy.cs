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
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Address) ||
                    Stock==null || Requests==null)
                throw new InputException("Name or address or stock empty");
        }
    }

}
