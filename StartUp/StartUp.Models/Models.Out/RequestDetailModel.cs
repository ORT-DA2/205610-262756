using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class RequestDetailModel
    {
        public List<PetitionBasicModel> Petitions { get; set; }
        public bool State { get; set; }

        public RequestDetailModel(Request request)
        {
            this.State = request.State;
            Petitions = new List<PetitionBasicModel>();
            foreach (var item in request.Petitions)
            {
                Petitions.Add(new PetitionBasicModel(item));
            }
        }
    }
}
