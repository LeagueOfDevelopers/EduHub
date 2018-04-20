using System;
using EduHubLibrary.Domain;

namespace EduHub.Models.NotificationsModels
{
    public class InvitationReceivedResponse
    {
        public InvitationReceivedResponse(string groupTitle, string inviterName, MemberRole suggestedRole)
        {
            GroupTitle = groupTitle;
            InviterName = inviterName;
            SuggestedRole = suggestedRole;
        }

        public string GroupTitle { get; }
        public string InviterName { get; }
        public MemberRole SuggestedRole { get; }
    }
}