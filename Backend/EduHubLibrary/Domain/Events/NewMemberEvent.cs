using System;
using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.Domain.Events
{
    public class NewMemberEvent : IEventInfo
    {
        public NewMemberEvent(Guid groupId, Guid newMemberId)
        {
            GroupId = groupId;
            NewMemberId = newMemberId;
        }

        public Guid GroupId { get; }
        public Guid NewMemberId { get; }

        public EventType GetEventType()
        {
            return EventType.NewMemberEvent;
        }
    }
}