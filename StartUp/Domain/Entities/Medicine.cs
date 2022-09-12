using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class Medicine
    {
        private string Code { get; set; }
        private string Name { get; set; }
        private List<string> Symptoms { get; set; }
        private string Presentation { get; set; }
        private int Amount { get; set; }
        private string Measure { get; set; }
        private int Price { get; set; }
        private int Stock { get; set; }
        private bool Prescription { get; set; }

    }
}
