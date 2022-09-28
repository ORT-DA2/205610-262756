using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class MedicineBasicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Presentation { get; set; }
        public int Amount { get; set; }
        public string Measure { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public bool Prescription { get; set; }

        public MedicineBasicModel(Medicine medicine)
        {
            this.Id = medicine.Id;
            this.Presentation = medicine.Presentation;
            this.Amount = medicine.Amount;
            this.Price = medicine.Price;
            this.Stock = medicine.Stock;
            this.Prescription = medicine.Prescription;
            this.Name = medicine.Name;
            this.Measure = medicine.Measure;
        }

        public override bool Equals(object? obj)
        {
            if (obj is MedicineBasicModel)
            {
                var otherMedicine = obj as MedicineBasicModel;

                return Id == otherMedicine.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
