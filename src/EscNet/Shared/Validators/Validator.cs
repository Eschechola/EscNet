using System;

namespace EscNet.Shared.Validators
{
    public static class Validator
    {
        public static bool ValidateStringIsNotNullOrEmpty(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new NullReferenceException("The text cannot be null or empty.");

            return true;
        }
    }
}
