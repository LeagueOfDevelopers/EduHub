using System;

namespace EduHub.Models.NotificationsModels
{
    public class CurriculumSuggestedResponse
    {
        public CurriculumSuggestedResponse(string curriculumLink, string groupTitle)
        {
            CurriculumLink = curriculumLink;
            GroupTitle = groupTitle;
        }

        public string CurriculumLink { get; }
        public string GroupTitle { get; }
    }
}