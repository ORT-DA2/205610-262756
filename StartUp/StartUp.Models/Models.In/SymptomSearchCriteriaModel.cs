﻿using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StartUp.Domain.SearchCriterias;

namespace StartUp.Models.Models.In
{
    public class SymptomSearchCriteriaModel
    {
        public string? SymptomDescription { get; set; }

        public SymptomSearchCriteria ToEntity()
        {
            return new SymptomSearchCriteria()
            {
                Symptom = this.SymptomDescription
            };
        }
    }
}
