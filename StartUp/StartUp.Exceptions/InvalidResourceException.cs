using System;
using System.Runtime.Serialization;

namespace StartUp.Exceptions
{
    public class InvalidResourceException : Exception
    {
        public InvalidResourceException()
        {
        }

        public InvalidResourceException(string message) : base(message)
        {
        }

        public InvalidResourceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidResourceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}