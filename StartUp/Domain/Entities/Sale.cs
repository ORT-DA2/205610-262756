using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class Sale
    {
        public int Id { get; set; }
        public List<InvoiceLine> Medicines { get; set; }
    }
}
