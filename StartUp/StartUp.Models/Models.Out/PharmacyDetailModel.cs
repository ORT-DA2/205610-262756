using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using StartUp.Models.Models.In;

namespace StartUp.Models.Models.Out
{
    public class PharmacyDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<MedicineBasicModel> Stock { get; set; }
        public List<RequestBasicModel> Requests { get; set; }
        public List<SaleBasicModel> Sales { get; set; }
        
        public List<PetitionBasicModel> Petitions { get; set; }

        

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
            Sales = new List<SaleBasicModel>();
            foreach (var item in pharmacy.Sales)
            {
                Sales.Add(new SaleBasicModel(item));
            }
            Petitions = new List<PetitionBasicModel>();
            foreach (var item in pharmacy.Petitions)
            {
                Petitions.Add(new PetitionBasicModel(item));
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
