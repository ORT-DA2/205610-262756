using StartUp.Exceptions;
using System.Collections.Generic;
using System.Linq;

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

        public void ValidateLengthString(string value, string message, int length)
        {
            if(value.Length > length)
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
