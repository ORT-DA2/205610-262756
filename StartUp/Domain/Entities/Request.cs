using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain
{
    public class Request
    {
        public int Id { get; set; }
        public List<Petition> Petitions { get; set; }
        public string State { get; set; }
        private Validator validator;

        public Request()
        {
            validator = new Validator();
        }
        public void isValidRequest()
        {
            validator.ValidatePetitions(Petitions, "Petitions empty");
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Request)obj);
        }

        protected bool Equals(Request other)
        {
            return Id == other?.Id;
        }
    }
}
