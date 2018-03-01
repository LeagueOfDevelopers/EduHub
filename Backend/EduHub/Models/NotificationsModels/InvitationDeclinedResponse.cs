using System;

namespace EduHub.Models.NotificationsModels
{
    public class InvitationDeclinedResponse
    {
        public InvitationDeclinedResponse(string groupTitle, Guid groupId, string invitedName, Guid invitedId)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
            InvitedName = invitedName;
            InvitedId = invitedId;
        }

        public string GroupTitle { get; }
        public Guid GroupId { get; }
        public string InvitedName { get; }
        public Guid InvitedId { get; }
    }
}