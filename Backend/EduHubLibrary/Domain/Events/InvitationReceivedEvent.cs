using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class InvitationReceivedEvent : EventInfoBase
    {
        public InvitationReceivedEvent(string groupTitle, string inviterName, int invitedId, MemberRole suggestedRole)
        {
            GroupTitle = groupTitle;
            InviterName = inviterName;
            InvitedId = invitedId;
            SuggestedRole = suggestedRole;
        }

        public string GroupTitle { get; }
        public string InviterName { get; }
        public int InvitedId { get; }
        public MemberRole SuggestedRole { get; }
    }
}
