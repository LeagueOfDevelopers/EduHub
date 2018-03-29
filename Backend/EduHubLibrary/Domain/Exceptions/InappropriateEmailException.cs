using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class InappropriateEmailException : Exception
    {
        public InappropriateEmailException(string expectedEmail, string actualEmail) :
            base($"Emails are not equal. Expected: {expectedEmail}, actual: {actualEmail}")
        {
        }

        public InappropriateEmailException(string message) : base(message)
        {
        }

        public InappropriateEmailException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InappropriateEmailException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}