using EduHubLibrary.Domain;
using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.EventBus.EventTypes
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

        public override EventType GetEventType()
        {
            return EventType.InvitationReceived;
        }
    }
}
