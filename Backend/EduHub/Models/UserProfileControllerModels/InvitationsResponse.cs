using System.Collections.Generic;
using EduHub.Models.Tools;

namespace EduHub.Models
{
    public class InvitationsResponse
    {
        public InvitationsResponse(IEnumerable<InvitationModel> invitations)
        {
            Invitations = invitations;
        }

        /// <summary>All user's invitations</summary>
        public IEnumerable<InvitationModel> Invitations { get; set; }
    }
}