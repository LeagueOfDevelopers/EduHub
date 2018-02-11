using System;

namespace EduHub.Models
{
    public class CreateGroupResponse
    {
        public CreateGroupResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}