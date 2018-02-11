using System;
using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.Domain.Events
{
    public class NewCurriculumEvent : IEventInfo
    {
        public NewCurriculumEvent(Guid groupId, string curriculum)
        {
            GroupId = groupId;
            Curriculum = curriculum;
        }

        public Guid GroupId { get; }
        public string Curriculum { get; }

        public EventType GetEventType()
        {
            return EventType.NewCurriculumEvent;
        }
    }
}