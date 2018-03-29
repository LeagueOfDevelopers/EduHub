using System.ComponentModel.DataAnnotations;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Data.KeyDtos
{
    public class KeyDto
    {
        public KeyDto(bool used, int value, string userEmail, KeyAppointment appointment)
        {
            Used = used;
            Value = value;
            UserEmail = userEmail;
            Appointment = appointment;
        }

        public KeyDto()
        {
        }

        [Key] public int Value { get; set; }

        public bool Used { get; set; }
        public string UserEmail { get; set; }
        public KeyAppointment Appointment { get; set; }
    }
}