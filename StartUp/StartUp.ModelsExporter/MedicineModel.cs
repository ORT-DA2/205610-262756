
namespace StartUp.ModelsExporter
{
    public class MedicineModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Presentation { get; set; }
        public int Amount { get; set; }
        public string Measure { get; set; }
        public int Price { get; set; }
        public bool Prescription { get; set; }
        public List<SymptomModel> Symptoms { get; set; }
    }
}
