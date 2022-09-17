using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class PetitionDetailModel
    {
        public string MedicineCode { get; set; }
        public int Amount { get; set; }

        public PetitionDetailModel(Petition petition)
        {
            this.MedicineCode = petition.MedicineCode;
            this.Amount = petition.Amount;
        }
    }
}
