using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class WrongKeyAppointmentException : Exception
    {
        public WrongKeyAppointmentException(KeyAppointment wrongType)
            : base($"Wrong type of using key: {wrongType}")
        {
        }

        public WrongKeyAppointmentException(string message) : base(message)
        {
        }

        public WrongKeyAppointmentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongKeyAppointmentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}