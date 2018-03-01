using System;

namespace EduHub.Models.NotificationsModels
{
    public class CurriculumSuggestedResponse
    {
        public CurriculumSuggestedResponse(string curriculumLink, string groupTitle, Guid groupId)
        {
            CurriculumLink = curriculumLink;
            GroupTitle = groupTitle;
            GroupId = groupId;
        }

        public string CurriculumLink { get; }
        public string GroupTitle { get; }
        public Guid GroupId { get; }
    }
}