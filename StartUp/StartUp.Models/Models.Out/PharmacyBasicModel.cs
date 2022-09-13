using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class PharmacyBasicModel
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public PharmacyBasicModel(Pharmacy pharmacy)
        {
            this.Name = pharmacy.Name;
            this.Address = pharmacy.Address;
        }
    }
}
