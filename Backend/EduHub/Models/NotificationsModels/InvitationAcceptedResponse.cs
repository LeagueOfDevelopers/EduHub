using System;

namespace EduHub.Models.NotificationsModels
{
    public class InvitationAcceptedResponse
    {
        public InvitationAcceptedResponse(string groupTitle, string invitedName)
        {
            GroupTitle = groupTitle;
            InvitedName = invitedName;
        }

        public string GroupTitle { get; }
        public string InvitedName { get; }
    }
}