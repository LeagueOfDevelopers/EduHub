using System.Collections.Generic;

namespace EduHub.Models
{
    public class MinUserForInvitationResponse
    {
        public MinUserForInvitationResponse(List<MinUserForInvitationItem> users)
        {
            Users = users;
        }

        public List<MinUserForInvitationItem> Users { get; }
    }
}