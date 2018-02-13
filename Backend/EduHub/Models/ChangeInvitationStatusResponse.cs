using System;

namespace EduHub.Models
{
    public class ChangeInvitationStatusResponse
    {
        public ChangeInvitationStatusResponse(Guid groupId)
        {
            GroupId = groupId;
        }

        public Guid GroupId { get; }
    }
}