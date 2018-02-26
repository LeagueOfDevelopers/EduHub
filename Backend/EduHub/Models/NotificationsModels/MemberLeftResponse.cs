using System;

namespace EduHub.Models.NotificationsModels
{
    public class MemberLeftResponse
    {
        public Guid GroupId { get; }
        public string GroupTitle { get; }
        public Guid UserId { get; }
        public string Username { get; }

        public MemberLeftResponse(Guid groupId, string groupTitle, Guid userId, string username)
        {
            GroupId = groupId;
            GroupTitle = groupTitle;
            UserId = userId;
            Username = username;
        }
    }
}
