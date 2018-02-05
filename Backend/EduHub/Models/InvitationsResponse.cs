using EduHub.Models.Tools;
using System.Collections.Generic;

namespace EduHub.Models
{
    public class InvitationsResponse
    {
        /// <summary>All user's invitations</summary>
        public IEnumerable<InvitationModel> Invitations { get; set; }

        public InvitationsResponse(IEnumerable<InvitationModel> invitations)
        {
            Invitations = invitations;
        }
    }
}
