using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class InvoiceLine
    {
        public int Id { get; set; }
        public Medicine Medicine { get; set; }
        public int Amount { get; set; }

    }
}
