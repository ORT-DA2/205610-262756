using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StartUp.Domain
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<Symptom> Symptoms { get; set; }
        public string Presentation { get; set; }
        public int Amount { get; set; }
        public string Measure { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public bool Prescription { get; set; }


        public Medicine() { }
        public void IsValidMedicine()
        {
            Validator validator = new Validator();
            validator.ValidateString(Code, "The Code can't be empty or only white spaces");
            validator.ValidateString(Name, "The Name can't be empty or only white spaces");
            validator.ValidateString(Measure, "The Name can't be empty or only white spaces");
            validator.ValidateString(Presentation, "The Name can't be empty or only white spaces");
            validator.ValidateSymptomsListNotNull(Symptoms, "The Symptoms list can't be null");
            validator.ValidateAmount(Amount, 1, "The amount of the medicine can't be less than 1");
            validator.ValidateAmount(Price, 1,"The price of the medicine can't be less than 1");
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Medicine)obj);
        }

        protected bool Equals(Medicine other)
        {
            return Code == other?.Code;
        }
    }
}
