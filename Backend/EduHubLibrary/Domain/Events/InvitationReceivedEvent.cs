using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class InvitationReceivedEvent : EventInfoBase
    {
        public InvitationReceivedEvent(string groupTitle, int groupId, string inviterName, int inviterId, 
            int invitedId, MemberRole suggestedRole)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
            InviterName = inviterName;
            InviterId = inviterId;
            InvitedId = invitedId;
            SuggestedRole = suggestedRole;
        }

        public string GroupTitle { get; }
        public int GroupId { get; }
        public string InviterName { get; }
        public int InviterId { get; }
        public int InvitedId { get; }
        public MemberRole SuggestedRole { get; }
    }
}
