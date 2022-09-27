using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class PetitionDetailModel
    {
        public int Id { get; set; }
        public string MedicineCode { get; set; }
        public int Amount { get; set; }

        public PetitionDetailModel(Petition petition)
        {
            this.MedicineCode = petition.MedicineCode;
            this.Amount = petition.Amount;
        }

        public override bool Equals(object? obj)
        {
            if (obj is PetitionDetailModel)
            {
                var otherPetition = obj as PetitionDetailModel;

                return Id == otherPetition.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
