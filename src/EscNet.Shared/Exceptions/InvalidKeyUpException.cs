using System;

namespace EscNet.Shared.Exceptions
{
    public class InvalidKeyUpException : Exception
    {
        public InvalidKeyUpException()
        {
        }

        public InvalidKeyUpException(string message) : base(message)
        {
        }

        public InvalidKeyUpException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
