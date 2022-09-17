using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class PetitionModel
    {
        public string MedicineCode { get; set; }
        public int Amount { get; set; }

        public Petition ToEntity()
        {
            return new Petition()
            {
                MedicineCode = MedicineCode,
                Amount = Amount
            };
        }
    }
}
