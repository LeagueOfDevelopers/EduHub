using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class MemberLeftEvent : EventInfoBase
    {
        public MemberLeftEvent(int groupId, string groupTitle, int userId, string username)
        {
            GroupId = groupId;
            GroupTitle = groupTitle;
            UserId = userId;
            Username = username;
        }

        public int GroupId { get; }
        public string GroupTitle { get; }
        public int UserId { get; }
        public string Username { get; }
    }
}
