using System;

namespace EscNet.Shared.Exceptions;

public class InvalidSMTPException : Exception
{
    public InvalidSMTPException()
    {
    }

    public InvalidSMTPException(string message) : base(message)
    {
    }

    public InvalidSMTPException(string message, Exception innerException) : base(message, innerException)
    {
    }
}