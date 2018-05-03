namespace EduHub.Models.NotificationsModels
{
    public class GroupIsFormedResponse
    {
        public GroupIsFormedResponse(string groupTitle)
        {
            GroupTitle = groupTitle;
        }

        public string GroupTitle { get; }
    }
}