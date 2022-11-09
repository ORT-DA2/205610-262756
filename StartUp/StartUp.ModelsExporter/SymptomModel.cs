

using StartUp.Domain;

namespace StartUp.ModelsExporter
{
    public class SymptomModel
    {
        public string SymptomDescription { get; set; }

        public SymptomModel(Symptom symtom)
        {
            SymptomDescription = symtom.SymptomDescription;
        }
    }
}
