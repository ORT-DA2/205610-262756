using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class Request
    {
        private List<Petition> Petitions { get; set; }
        private bool State { get; set; }
    }
}
