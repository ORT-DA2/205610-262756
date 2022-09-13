using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class PetitionBasicModel
    {
        public string MedicineCode { get; set; }
        public int Amount { get; set; }

        public PetitionBasicModel(Petition petition)
        {
            this.MedicineCode = petition.MedicineCode;
            this.Amount = petition.Amount;
        }
    }
}
