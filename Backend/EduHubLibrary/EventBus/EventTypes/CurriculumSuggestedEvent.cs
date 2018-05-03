using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.EventBus.EventTypes
{
    public class CurriculumSuggestedEvent : EventInfoBase
    {
        public CurriculumSuggestedEvent(string curriculumLink, string groupTitle, int groupId)
        {
            CurriculumLink = curriculumLink;
            GroupTitle = groupTitle;
            GroupId = groupId;
        }

        public string CurriculumLink { get; }
        public string GroupTitle { get; }
        public int GroupId { get; }

        public override EventType GetEventType()
        {
            return EventType.CurriculumSuggested;
        }
    }
}
