using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.Models.Models.Out
{
    public class SymptomDetailModel
    {
        public int Id { get; set; }
        public string SymptomDescription { get; set; }

        public SymptomDetailModel(Symptom symptom)
        {
            this.Id = symptom.Id;
            this.SymptomDescription = symptom.SymptomDescription;
        }

        public override bool Equals(object? obj)
        {
            if (obj is SymptomDetailModel)
            {
                var otherSymptom = obj as SymptomDetailModel;

                return Id == otherSymptom.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
