using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class RequestDetailModel
    {
        public int Id { get; set; }
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

        public override bool Equals(object? obj)
        {
            if (obj is RequestDetailModel)
            {
                var otherRequest = obj as RequestDetailModel;

                return Id == otherRequest.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
