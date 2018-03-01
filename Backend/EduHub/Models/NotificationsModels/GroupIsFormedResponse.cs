using System;

namespace EduHub.Models.NotificationsModels
{
    public class GroupIsFormedResponse
    {
        public GroupIsFormedResponse(string groupTitle, Guid groupId)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
        }

        public string GroupTitle { get; }
        public Guid GroupId { get; }
    }
}