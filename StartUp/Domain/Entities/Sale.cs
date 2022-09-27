using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class Sale
    {
        public int Id { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }

        public void isValidSale()
        {
            if (InvoiceLines == null)
                throw new InputException("Medicines empty");
        }
    }
}
