using StartUp.Domain;

namespace StartUp.ModelsExporter
{
    public class MedicineModelExport
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Presentation { get; set; }
        public int Amount { get; set; }
        public string Measure { get; set; }
        public int Price { get; set; }
        public bool Prescription { get; set; }
        public int Stock { get; set; }
        public List<Symptom> Symptoms { get; set; }
        
        public MedicineModelExport(Medicine medicine)
            {
                this.Presentation = medicine.Presentation;
                this.Amount = medicine.Amount;
                this.Price = medicine.Price;
                this.Stock = medicine.Stock;
                this.Prescription = medicine.Prescription;
                this.Name = medicine.Name;
                this.Measure = medicine.Measure;
                this.Symptoms = medicine.Symptoms;
                this.Code = medicine.Code;
            }

            public override bool Equals(object? obj)
            {
                if (obj is MedicineModelExport)
                {
                    var otherMedicine = obj as MedicineModelExport;

                    return Name == otherMedicine.Name;
                }
                else
                {
                    return false;
                }
            }
        }
}