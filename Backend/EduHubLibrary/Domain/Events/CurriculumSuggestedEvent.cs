using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
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
    }
}
