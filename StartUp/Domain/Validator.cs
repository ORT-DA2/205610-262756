using StartUp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
