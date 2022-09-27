using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class SaleDetailModel
    {
        public int Id { get; set; }
        public List<InvoiceLineBasicModel> Medicines { get; set; }

        public SaleDetailModel(Sale sale)
        {
            this.Id = sale.Id;
            Medicines = new List<InvoiceLineBasicModel>();
            foreach (var item in sale.Medicines)
            {
                Medicines.Add(new InvoiceLineBasicModel(item));
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is SaleDetailModel)
            {
                var otherSale = obj as SaleDetailModel;

                return Id == otherSale.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
