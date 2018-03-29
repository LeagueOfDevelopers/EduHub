using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class CurriculumDeclinedEvent : EventInfoBase
    {
        public CurriculumDeclinedEvent(string groupTitle, int groupId, string declinedName, int declinedId)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
            DeclinedName = declinedName;
            DeclinedId = declinedId;
        }

        public string GroupTitle { get; }
        public int GroupId { get; }
        public string DeclinedName { get; }
        public int DeclinedId { get; }
    }
}
