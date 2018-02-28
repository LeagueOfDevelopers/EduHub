using System;
using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.Domain.Events
{
    public class NewCurriculumEvent : EventInfoBase
    {
        public NewCurriculumEvent(Guid groupId, string curriculum)
        {
            GroupId = groupId;
            Curriculum = curriculum;
        }

        public Guid GroupId { get; }
        public string Curriculum { get; }

        public override EventType GetEventType()
        {
            return EventType.NewCurriculumEvent;
        }
    }
}