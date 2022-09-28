using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.Models.Models.In
{
    public class SymptomSearchCriteriaModel
    {
        public string? SymptomDescription { get; set; }

        public Symptom ToEntity()
        {
            return new Symptom()
            {
                SymptomDescription = this.SymptomDescription
            };
        }
    }
}
