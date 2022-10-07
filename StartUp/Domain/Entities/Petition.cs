

namespace StartUp.Domain
{
    public class Petition
    {
        public int Id { get; set; }
        public string MedicineCode { get; set; }
        public int Amount { get; set; }


        public Petition() { }
        public void IsValidPetition()
        {
            Validator validator = new Validator();
            validator.ValidateString(MedicineCode, "The code can't be null");
            validator.ValidateAmount(Amount, 1, "The Amount can't be less than 1");
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Petition)obj);
        }

        protected bool Equals(Petition other)
        {
            return Id == other?.Id;
        }
    }
}
