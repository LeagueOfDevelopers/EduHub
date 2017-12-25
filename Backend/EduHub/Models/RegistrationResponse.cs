using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class RegistrationResponse
    {
        public Guid Id { get; set; }

        public RegistrationResponse(Guid id)
        {
            Id = id;
        }
    }
}
