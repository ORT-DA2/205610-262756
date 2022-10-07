using System;

namespace StartUp.Domain.Entities
{
    public class TokenAccess
    {
        public int Id { get; set; }
        public Guid Token { get; set; }
        public User User { get; set; }

        public TokenAccess() { }
        public void IsValidTokenAccess()
        {
            Validator validator = new Validator();
            
            validator.ValidateUserNotNull(User, "User empty");
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TokenAccess)obj);
        }

        protected bool Equals(TokenAccess other)
        {
            return Token == other?.Token;
        }

    }

}
