using StartUp.Exceptions;
using System.Collections.Generic;

namespace StartUp.Domain
{
    public class Request
    {
        public int Id { get; set; }
        public List<Petition> Petitions { get; set; }
        public string State { get; set; }

        public Request() { }
        public void isValidRequest()
        {
            if(Petitions == null)
            {
                throw new InputException("Petitions empty");
            }
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
