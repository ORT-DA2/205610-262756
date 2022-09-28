using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class SaleBasicModel
    {
        public int Id { get; set; }
       

        public SaleBasicModel(Sale sale)
        {
            this.Id = sale.Id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is SaleBasicModel)
            {
                var otherSale = obj as SaleBasicModel;

                return Id == otherSale.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
