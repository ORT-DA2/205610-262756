using StartUp.Domain;

namespace StartUp.Models.Models.Out
{
    public class PharmacyBasicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public PharmacyBasicModel(Pharmacy pharmacy)
        {
            this.Id = pharmacy.Id;
            this.Name = pharmacy.Name;
            this.Address = pharmacy.Address;
        }

        public override bool Equals(object? obj)
        {
            if (obj is PharmacyBasicModel)
            {
                var otherPharmacy = obj as PharmacyBasicModel;

                return Id == otherPharmacy.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
