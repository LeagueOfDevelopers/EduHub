using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.EventBus.EventTypes
{
    public class NewCreatorEvent : EventInfoBase
    {
        public NewCreatorEvent(int groupId, string groupTitle, string exCreatorUsername, string newCreatorUsername)
        {
            GroupId = groupId;
            GroupTitle = groupTitle;
            ExCreatorUsername = exCreatorUsername;
            NewCreatorUsername = newCreatorUsername;
        }

        public int GroupId { get; }
        public string GroupTitle { get; }
        public string ExCreatorUsername { get; }
        public string NewCreatorUsername { get; }

        public override EventType GetEventType()
        {
            return EventType.NewCreator;
        }
    }
}
