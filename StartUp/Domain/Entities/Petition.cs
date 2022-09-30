using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class Petition
    {
        public int Id { get; set; }
        public string MedicineCode { get; set; }
        public int Amount { get; set; }


        public Petition() { }
        public void isValidPetition()
        {
            if (string.IsNullOrEmpty(MedicineCode))
                throw new InputException("Enter a Petition");
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Petition)obj);
        }

        protected bool Equals(Petition other)
        {
            return Id == other?.Id;
        }
    }
}
