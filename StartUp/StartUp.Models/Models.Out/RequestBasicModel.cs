using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.Out
{
    public class RequestBasicModel
    {
        public int Id { get; set; }
        public string State { get; set; }

        public RequestBasicModel(Request request)
        {
            this.Id = request.Id;
            this.State = request.State;
        }

        public override bool Equals(object? obj)
        {
            if (obj is RequestBasicModel)
            {
                var otherRequest = obj as RequestBasicModel;

                return Id == otherRequest.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
