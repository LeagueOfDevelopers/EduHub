using System;
using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.Domain.Events
{
    public class NewMemberEvent : EventInfoBase
    {
        public NewMemberEvent(int groupId, int newMemberId)
        {
            GroupId = groupId;
            NewMemberId = newMemberId;
        }

        public int GroupId { get; }
        public int NewMemberId { get; }

        public override EventType GetEventType()
        {
            return EventType.NewMemberEvent;
        }
    }
}