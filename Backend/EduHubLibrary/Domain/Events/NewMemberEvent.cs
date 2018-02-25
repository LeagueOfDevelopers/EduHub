using System;
using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.Domain.Events
{
    public class NewMemberEvent : EventInfoBase
    {
        public NewMemberEvent(Guid groupId, Guid newMemberId)
        {
            GroupId = groupId;
            NewMemberId = newMemberId;
        }

        public Guid GroupId { get; }
        public Guid NewMemberId { get; }

        public override EventType GetEventType()
        {
            return EventType.NewMemberEvent;
        }
    }
}