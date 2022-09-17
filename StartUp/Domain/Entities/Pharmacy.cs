using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class Pharmacy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Medicine> Stock { get; set; }
        public List<Request> Requests { get; set; }
    }
}
