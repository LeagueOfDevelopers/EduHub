namespace EduHubLibrary.Domain
{
    public class Key
    {
        public Key(string userEmail, KeyAppointment keyAppointment, int value = 0)
        {
            Value = value;
            Used = false;
            UserEmail = userEmail;
            Appointment = keyAppointment;
        }

        internal Key(string userEmail, KeyAppointment keyAppointment, bool used, int value = 0)
        {
            Value = value;
            Used = false;
            UserEmail = userEmail;
            Appointment = keyAppointment;
            Used = used;
        }

        public bool Used { get; private set; }
        public int Value { get; internal set; }
        public string UserEmail { get; }
        public KeyAppointment Appointment { get; }

        public void UseKey()
        {
            Used = true;
        }
    }
}