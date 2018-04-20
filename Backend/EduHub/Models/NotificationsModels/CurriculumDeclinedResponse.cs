using System;

namespace EduHub.Models.NotificationsModels
{
    public class CurriculumDeclinedResponse
    {
        public CurriculumDeclinedResponse(string groupTitle, string declinedName)
        {
            GroupTitle = groupTitle;
            DeclinedName = declinedName;
        }

        public string GroupTitle { get; }
        public string DeclinedName { get; }
    }
}