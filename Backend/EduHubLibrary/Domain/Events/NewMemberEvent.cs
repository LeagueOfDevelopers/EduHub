using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class NewMemberEvent : EventInfoBase
    {
        public NewMemberEvent(int groupId, string groupTitle, string username)
        {
            GroupId = groupId;
            GroupTitle = groupTitle;
            Username = username;
        }

        public int GroupId { get; }
        public string GroupTitle { get; }
        public string Username { get; }
    }
}
