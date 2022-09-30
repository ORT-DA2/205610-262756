using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class PharmacyModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Medicine> Stock { get; set; }
        public List<Request> Requests { get; set; }

        public Pharmacy ToEntity()
        {
            return new Pharmacy()
            {
                Name = this.Name,
                Address = this.Address,
                Stock = this.Stock,
                Requests = this.Requests
            };
        }
    }
}
