using Domain;
using StartUp.Domain.Entities;
using StartUp.Exceptions;
using System;

namespace StartUp.Domain
{
    public class Validator 
    {
        public void ValidateString(string value, string message)
        {
            if (value.Length == 0 || string.IsNullOrWhiteSpace(value))
            {
                throw new InputException(message);
            }
        }

        public void ValidateToken(Guid token, string message)
        {
            if (string.IsNullOrEmpty(token.ToString()))
            {
                throw new InputException(message);
            }
        }

        public void ValidateTokenAccess(TokenAccess token, string message)
        {
            if (token == null)
            {
                throw new InputException(message);
            }
        }

        public void ValidateStringValue(string value, string message)
        {
            if (string.IsNullOrEmpty(value.ToString()))
            {
                throw new InputException(message);
            }
        }

        public void ValidateLengthString(string value, string message, int length)
        {
            if(value.Length > length)
            {
                throw new InputException(message);
            }
        }

        public void ValidateUserNotNull(User user, string message)
        {
            if (user == null)
            {
                throw new InputException(message);
            }
        }

        public void ValidateSessionNotNull(Session session, string message)
        {
            if (session == null)
            {
                throw new InputException(message);
            }
        }

        public void ValidateStringEquals(string a, string b, string message)
        {
            if (a != b)
            {
                throw new InputException(message);
            }
        }

        /*
        public void ValidateNotNull(List<T> value, string message)
        {
            if(value == null)
            {
                throw new InputException(message);
            }
        }*/
    }
}
