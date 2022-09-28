using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class PetitionBasicModel
    {
        public int Id { get; set; }
        public string MedicineCode { get; set; }

        public PetitionBasicModel(Petition petition)
        {
            this.Id = petition.Id;
            this.MedicineCode = petition.MedicineCode;
        }

        public override bool Equals(object? obj)
        {
            if (obj is PetitionBasicModel)
            {
                var otherPetition = obj as PetitionBasicModel;

                return Id == otherPetition.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
