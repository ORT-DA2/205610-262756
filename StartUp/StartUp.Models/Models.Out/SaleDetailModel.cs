using StartUp.Domain;
using System.Collections.Generic;

namespace StartUp.Models.Models.Out
{
    public class SaleDetailModel
    {
        public int Id { get; set; }
        public List<InvoiceLineDetailModel> InvoiceLines { get; set; }
        public int Code { get; set; }

        public SaleDetailModel(Sale sale)
        {
            this.Id = sale.Id;
            InvoiceLines = new List<InvoiceLineDetailModel>();
            foreach (var item in sale.InvoiceLines)
            {
                InvoiceLines.Add(new InvoiceLineDetailModel(item));
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
