namespace EduHub.Models.NotificationsModels
{
    public class NewCreatorResponse
    {
        public NewCreatorResponse(string groupTitle, string exCreatorUsername, string newCreatorUsername)
        {
            GroupTitle = groupTitle;
            ExCreatorUsername = exCreatorUsername;
            NewCreatorUsername = newCreatorUsername;
        }

        public string GroupTitle { get; }
        public string ExCreatorUsername { get; }
        public string NewCreatorUsername { get; }
    }
}