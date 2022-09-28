using StartUp.Exceptions;

namespace StartUp.Domain
{
    public class Symptom
    {
        public int Id { get; set; }
        public string SymptomDescription { get; set; }


        public Symptom() { }
        public void isValidSymptom()
        {
            if (string.IsNullOrEmpty(SymptomDescription))
                throw new InputException("Enter a symptom description");
        }
    }
}