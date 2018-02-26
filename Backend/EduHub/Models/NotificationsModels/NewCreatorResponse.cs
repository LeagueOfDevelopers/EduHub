using System;

namespace EduHub.Models.NotificationsModels
{
    public class NewCreatorResponse
    {
        public Guid GroupId { get; }
        public string GroupTitle { get; }
        public Guid ExCreator { get; }
        public string ExCreatorUsername { get; }
        public Guid NewCreator { get; }
        public string NewCreatorUsername { get; }

        public NewCreatorResponse(Guid groupId, string groupTitle, Guid exCreator, 
            string exCreatorUsername, Guid newCreator, string newCreatorUsername)
        {
            GroupId = groupId;
            GroupTitle = groupTitle;
            ExCreator = exCreator;
            ExCreatorUsername = exCreatorUsername;
            NewCreator = newCreator;
            NewCreatorUsername = newCreatorUsername;
        }
    }
}
