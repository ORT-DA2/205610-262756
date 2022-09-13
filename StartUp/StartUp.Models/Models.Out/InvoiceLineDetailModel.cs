using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class InvoiceLineDetailModel
    {
        public MedicineBasicModel Medicine { get; set; }
        public int Amount { get; set; }
        public InvoiceLineDetailModel(InvoiceLine line)
        {
            Medicine = new MedicineBasicModel(line.Medicine);
            this.Amount = line.Amount;
        }
    }
}
