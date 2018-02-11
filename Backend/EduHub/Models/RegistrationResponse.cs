using System;

namespace EduHub.Models
{
    public class RegistrationResponse
    {
        public RegistrationResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}