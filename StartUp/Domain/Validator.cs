using StartUp.Domain.Entities;
using StartUp.Exceptions;
using System;
using System.Collections.Generic;

namespace StartUp.Domain
{
    public class Validator 
    {
        public void ValidateString(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length == 0)
            {
                throw new InputException(message);
            }
        }

        public void ValidateRoleIsNotNull(Role role, string message)
        {
            if(role == null)
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

        public void ValidateContains(string roles, string permissions, string message)
        {
            string[] rolesArray = permissions.Split(",");
            foreach (var role in rolesArray)
            {
                if (!roles.Contains(role))
                {
                    throw new InputException(message);
                }
            }
        }

        public void ValidateArray(string[] array, string message)
        {
            if(array == null || array.Length == 0)
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

        public void ValidateInvitationNotNull(Invitation invitation, string message)
        {
            if (invitation == null)
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
