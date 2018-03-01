using System;

namespace EduHub.Models.NotificationsModels
{
    public class CurriculumDeclinedResponse
    {
        public CurriculumDeclinedResponse(string groupTitle, Guid groupId, string declinedName, Guid declinedId)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
            DeclinedName = declinedName;
            DeclinedId = declinedId;
        }

        public string GroupTitle { get; }
        public Guid GroupId { get; }
        public string DeclinedName { get; }
        public Guid DeclinedId { get; }
    }
}