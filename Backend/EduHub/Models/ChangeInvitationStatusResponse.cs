using System;

namespace EduHub.Models
{
    public class ChangeInvitationStatusResponse
    {
        public ChangeInvitationStatusResponse(int groupId)
        {
            GroupId = groupId;
        }

        public int GroupId { get; }
    }
}