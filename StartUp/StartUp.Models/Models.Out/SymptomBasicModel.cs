using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.Models.Models.Out
{
    public class SymptomBasicModel
    {
        public int Id { get; set; }
        public string SymptomDescription { get; set; }

        public SymptomBasicModel(Symptom symptom)
        {
            this.Id = symptom.Id;
            this.SymptomDescription = symptom.SymptomDescription;
        }

        public override bool Equals(object? obj)
        {
            if (obj is SymptomBasicModel)
            {
                var otherSymptom = obj as SymptomBasicModel;

                return Id == otherSymptom.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
