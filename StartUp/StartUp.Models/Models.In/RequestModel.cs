using StartUp.Domain;
using System.Collections.Generic;

namespace StartUp.Models.Models.In
{
    public class RequestModel
    {
        public List<Petition> Petitions { get; set; }
        public string State { get; set; }

        public Request ToEntity()
        {
            return new Request()
            {
                Petitions = this.Petitions,
                State = this.State
            };
        }
    }
}
