using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class GetInvitationsResponse
    {
        public IEnumerable<Invitation> Invitations { get; set; }

        public GetInvitationsResponse(IEnumerable<Invitation> invitations)
        {
            Invitations = invitations;
        }
    }
}
