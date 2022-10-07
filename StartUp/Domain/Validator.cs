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
            if (role == null)
            {
                throw new InputException(message);
            }
        }

        public void ValidatePetitions(List<Petition> petitions, string message)
        {
            if (petitions == null)
            {
                throw new InputException(message);
            }
        }

        public void ValidatePasswordValid(string password, string message, int length)
        {
            ValidateString(password, "Password empty");
            if (password.Length < length)
            {
                throw new InputException("The password must have at least 8 characters");
            }

            if (!ContainSpecialCharacters(password))
            {
                throw new InputException(message);
            }
        }

        public void ValidateSaleNotNull(Sale saleSaved, string message)
        {
            if (saleSaved == null)
            {
                throw new ResourceNotFoundException(message);
            }
        }

        private bool ContainSpecialCharacters(string password)
        {
            ValidateString(password, "Password empty");

            string stringsValid = "(?=.*[*?¡!#$%&])";
            foreach (char c in password)
            {
                if (stringsValid.Contains(c))
                {
                    return true;
                }
            }
            return false;

        }

        public void ValidateContainsRolesCorrect(string roles, string permissions, string message)
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

        public void ValidatePetitionNotNull(Petition petitionSaved, string message)
        {
            if (petitionSaved == null)
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
            if (value.Length > length)
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

        public void ValidateUserNull(User user, string message)
        {
            if (user == null)
            {
                throw new InputException(message);
            }
        }

        public void ValidateListPharmacyNotNull(List<Pharmacy> list, string message)
        {
            if(list == null)
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

        public void ValidateMedicineListNotNull(List<Medicine> value, string message)
        {
            if (value == null)
            {
                throw new InputException(message);
            }
        }

        public void ValidateSymptomsListNotNull(List<Symptom> value, string message)
        {
            if (value == null)
            {
                throw new InputException(message);
            }
        }

        public void ValidatePharmacyNotNull(Pharmacy pharmacy, string message)
        {
            if (pharmacy == null)
            {
                throw new InputException(message);
            }
        }

        public void ValidateRequestNotNull(Request request, string message)
        {
            if (request == null)
            {
                throw new InputException(message);
            }
        }

        public void ValidateAmount(int value, int min, string message)
        {
            if (value < min)
            {
                throw new InputException(message);
            }
        }

        public void ValidateMedicineNotNull(Medicine med, string message)
        {
            if (med == null)
            {
                throw new InputException(message);
            }
        }
    }
}
