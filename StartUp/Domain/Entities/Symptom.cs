using StartUp.Exceptions;

namespace StartUp.Domain
{
    public class Symptom
    {
        public int Id { get; set; }
        public string SymptomDescription { get; set; }


        public Symptom() { }
        public void IsValidSymptom()
        {
            if (string.IsNullOrEmpty(SymptomDescription))
                throw new InputException("Enter a symptom description");
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Symptom)obj);
        }

        protected bool Equals(Symptom other)
        {
            return SymptomDescription == other?.SymptomDescription;
        }
    }
}