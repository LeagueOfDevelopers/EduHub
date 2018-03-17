using EduHubLibrary.Domain;
using System.ComponentModel.DataAnnotations;

namespace EduHubLibrary.Data.KeyDtos
{
    public class KeyDto
    {
        [Key]
        public int Value { get; set; }
        public bool Used { get; set; }
        public string UserEmail { get; set; }
        public KeyAppointment Appointment { get; set; }

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
    }
}
