using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class RequestBasicModel
    {
        public bool State { get; set; }

        public RequestBasicModel(Request request)
        {
            this.State = request.State;
        }
    }
}
