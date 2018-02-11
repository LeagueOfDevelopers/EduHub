using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

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
