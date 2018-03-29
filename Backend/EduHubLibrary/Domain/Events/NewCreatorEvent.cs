using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class NewCreatorEvent : EventInfoBase
    {
        public NewCreatorEvent(int groupId, string groupTitle, int exCreator,
            string exCreatorUsername, int newCreator, string newCreatorUsername)
        {
            GroupId = groupId;
            GroupTitle = groupTitle;
            ExCreator = exCreator;
            ExCreatorUsername = exCreatorUsername;
            NewCreator = newCreator;
            NewCreatorUsername = newCreatorUsername;
        }

        public int GroupId { get; }
        public string GroupTitle { get; }
        public int ExCreator { get; }
        public string ExCreatorUsername { get; }
        public int NewCreator { get; }
        public string NewCreatorUsername { get; }
    }
}
