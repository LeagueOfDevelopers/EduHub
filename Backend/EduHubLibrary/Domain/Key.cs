using System;

namespace EduHubLibrary.Domain
{
    public class Key
    {
        public Key(string userEmail, KeyAppointment keyAppointment)
        {
            Value = Guid.NewGuid();
            Used = false;
            UserEmail = userEmail;
            Appointment = keyAppointment;
        }

        public bool Used { get; private set; }
        public Guid Value { get; }
        public string UserEmail { get; }
        public KeyAppointment Appointment { get; }

        public void UseKey()
        {
            Used = true;
        }
    }
}