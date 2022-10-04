using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class RequestModel
    {
        public List<Petition> Petitions { get; set; }

        public Request ToEntity()
        {
            return new Request()
            {
                Petitions = this.Petitions
            };
        }
    }
}
