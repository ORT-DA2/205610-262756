using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class Request
    {
        public List<Petition> Petitions { get; set; }
        public bool State { get; set; }
    }
}
