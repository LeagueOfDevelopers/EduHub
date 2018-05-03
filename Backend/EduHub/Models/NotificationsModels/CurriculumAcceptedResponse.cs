namespace EduHub.Models.NotificationsModels
{
    public class CurriculumAcceptedResponse
    {
        public CurriculumAcceptedResponse(string groupTitle)
        {
            GroupTitle = groupTitle;
        }

        public string GroupTitle { get; }
    }
}