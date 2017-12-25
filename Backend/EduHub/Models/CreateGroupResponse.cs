using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class CreateGroupResponse
    {
        public Guid Id { get; set; }

        public CreateGroupResponse(Guid id)
        {
            Id = id;
        }
    }
}
