using System;
using EduHubLibrary.Domain;

namespace EduHub.Models.NotificationsModels
{
    public class InvitationReceivedResponse
    {
        public InvitationReceivedResponse(string groupTitle,
            Guid groupId, string inviterName, Guid inviterId, MemberRole suggestedRole)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
            InviterName = inviterName;
            InviterId = inviterId;
            SuggestedRole = suggestedRole;
        }

        public string GroupTitle { get; }
        public Guid GroupId { get; }
        public string InviterName { get; }
        public Guid InviterId { get; }
        public MemberRole SuggestedRole { get; }
    }
}