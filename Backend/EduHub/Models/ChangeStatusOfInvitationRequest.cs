using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class ChangeStatusOfInvitationRequest
    {
        public Guid UserId {get; set;}
        public Guid InvitationId { get; set; }
        public InvitationStatus Status { get; set; }
    }
}
