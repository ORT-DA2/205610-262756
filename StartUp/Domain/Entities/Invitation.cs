using StartUp.Exceptions;

namespace StartUp.Domain
{
    public class Invitation
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Rol { get; set; }
        public int Code { get; set; }
        public string State { get; set; }
        public Pharmacy Pharmacy { get; set; }

        public Invitation() { }

        public void IsValidInvitation()
        {
            if(string.IsNullOrWhiteSpace(UserName) || UserName.Length == 0)
            {
                throw new InputException("User name can not be empty or all spaces");
            }
            if (string.IsNullOrWhiteSpace(Rol) || Rol.Length == 0)
            {
                throw new InputException("User role can not be empty or all spaces");
            }
        }
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Invitation)obj);
        }

        protected bool Equals(Invitation other)
        {
            return Code == other?.Code;
        
        }
    }
}
