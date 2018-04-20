using System;

namespace EduHub.Models.NotificationsModels
{
    public class MemberLeftResponse
    {
        public MemberLeftResponse(string groupTitle, string username)
        {
            GroupTitle = groupTitle;
            Username = username;
        }
        
        public string GroupTitle { get; }
        public string Username { get; }
    }
}