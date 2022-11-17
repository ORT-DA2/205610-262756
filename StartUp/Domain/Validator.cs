using StartUp.Exceptions;

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

        public void ValidateLengthString(string value, string message, int length)
        {
            if (value.Length > length)
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

        public void ValidateAmount(int value, int min, string message)
        {
            if (value < min)
            {
                throw new InputException(message);
            }
        }

    }
}
