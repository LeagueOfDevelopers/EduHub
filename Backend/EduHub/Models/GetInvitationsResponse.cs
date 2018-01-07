using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class GetInvitationsResponse
    {
        /// <summary>All user's invitations</summary>
        public IEnumerable<Invitation> Invitations { get; set; }

        public GetInvitationsResponse(IEnumerable<Invitation> invitations)
        {
            Invitations = invitations;
        }
    }
}
