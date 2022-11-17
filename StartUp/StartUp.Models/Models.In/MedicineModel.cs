using StartUp.Domain;
using System.Collections.Generic;

namespace StartUp.Models.Models.In
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
        public List<Symptom> Symptoms { get; set; }

        public Medicine ToEntity()
        {
            return new Medicine()
            {
                Code = this.Code,
                Name = this.Name,
                Presentation = this.Presentation,
                Amount = this.Amount,   
                Measure = this.Measure, 
                Price = this.Price,
                Prescription = this.Prescription,
                Symptoms = this.Symptoms
            };
        }
    }
}
