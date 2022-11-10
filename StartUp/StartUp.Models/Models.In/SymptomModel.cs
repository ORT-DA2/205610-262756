using StartUp.Domain;

namespace StartUp.Models.Models.In
{
    public class SymptomModel
    {
        public string SymptomDescription { get; set; }

        public Symptom ToEntity()
        {
            return new Symptom()
            {
                SymptomDescription = this.SymptomDescription
            };
        }
    }
}
