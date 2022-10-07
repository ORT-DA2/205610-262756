﻿using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class InvoiceLineSearchCriteriaModel
    {
        public Medicine? Medicine { get; set; }
        public int? Amount { get; set; }

        public InvoiceLineSearchCriteria ToEntity()
        {
            return new InvoiceLineSearchCriteria()
            {
                Amount = this.Amount,
                Medicine = this.Medicine
            };
        }
    }
}
