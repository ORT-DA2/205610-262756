using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class SaleDetailModel
    {
        public int Id { get; set; }
        public List<InvoiceLineBasicModel> InvoiceLines { get; set; }
        public int Code { get; set; }

        public SaleDetailModel(Sale sale)
        {
            this.Id = sale.Id;
            InvoiceLines = new List<InvoiceLineBasicModel>();
            foreach (var item in sale.InvoiceLines)
            {
                InvoiceLines.Add(new InvoiceLineBasicModel(item));
            }
            Code = sale.Code;
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
