using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.EventBus.EventTypes
{
    public class InvitationAcceptedEvent : EventInfoBase
    {
        public InvitationAcceptedEvent(string groupTitle, string invitedName, int senderId)
        {
            GroupTitle = groupTitle;
            InvitedName = invitedName;
            SenderId = senderId;
        }

        public string GroupTitle { get; }
        public string InvitedName { get; }
        public int SenderId { get; }

        public override EventType GetEventType()
        {
            return EventType.InvitationAccepted;
        }
    }
}
