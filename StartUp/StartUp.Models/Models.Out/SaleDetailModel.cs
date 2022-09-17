using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class SaleDetailModel
    {
        public List<InvoiceLineBasicModel> Medicines { get; set; }

        public SaleDetailModel(Sale sale)
        {
            Medicines = new List<InvoiceLineBasicModel>();
            foreach (var item in sale.Medicines)
            {
                Medicines.Add(new InvoiceLineBasicModel(item));
            }
        }
    }
}
