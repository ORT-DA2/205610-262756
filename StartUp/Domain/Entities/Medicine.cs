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

        public void isValidMedicine()
        {
            if (string.IsNullOrEmpty(Code) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Presentation)
                || string.IsNullOrEmpty(Measure) || Symptoms == null)
                throw new InputException("Enter a Medicine");
        }
    }
}
