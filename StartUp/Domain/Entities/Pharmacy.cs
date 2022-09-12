using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class Pharmacy
    {
        private string Name { get; set; }
        private string Address { get; set; }
        private List<Medicine> Stock { get; set; }
        private List<Request> Requests { get; set; }
    }
}
