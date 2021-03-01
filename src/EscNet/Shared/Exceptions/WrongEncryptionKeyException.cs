using System;

namespace EscNet.Shared.Exceptions
{
    public class WrongEncryptionKeyException : Exception
    {
        public WrongEncryptionKeyException()
        {    
        }

        public WrongEncryptionKeyException(string message) : base(message)
        {    
        }

        public WrongEncryptionKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}