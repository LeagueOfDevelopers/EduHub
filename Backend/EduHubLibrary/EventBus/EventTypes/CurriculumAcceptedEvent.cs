﻿using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.EventBus.EventTypes
{
    public class CurriculumAcceptedEvent : EventInfoBase
    {
        public CurriculumAcceptedEvent(string groupTitle, int groupId)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
        }

        public string GroupTitle { get; }
        public int GroupId { get; }

        public override EventType GetEventType()
        {
            return EventType.CurriculumAccepted;
        }
    }
}