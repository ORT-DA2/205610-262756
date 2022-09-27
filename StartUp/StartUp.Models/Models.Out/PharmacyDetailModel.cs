using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class PharmacyDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<MedicineBasicModel> Stock { get; set; }
        public List<RequestBasicModel> Requests { get; set; }

        public PharmacyDetailModel(Pharmacy pharmacy)
        {
            this.Name = pharmacy.Name;
            this.Address = pharmacy.Address;
            Stock = new List<MedicineBasicModel>();
            foreach (var item in pharmacy.Stock)
            {
                Stock.Add(new MedicineBasicModel(item));
            }
            Requests = new List<RequestBasicModel>();
            foreach (var item in pharmacy.Requests)
            {
                Requests.Add(new RequestBasicModel(item));
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is PharmacyDetailModel)
            {
                var otherPharmacy = obj as PharmacyDetailModel;

                return Id == otherPharmacy.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
