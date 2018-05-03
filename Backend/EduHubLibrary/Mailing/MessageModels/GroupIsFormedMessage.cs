namespace EduHubLibrary.Mailing.MessageModels
{
    public class GroupIsFormedMessage
    {
        public GroupIsFormedMessage(string groupTitle, string receiverName)
        {
            GroupTitle = groupTitle;
            ReceiverName = receiverName;
        }

        public string GroupTitle { get; }
        public string ReceiverName { get; }
    }
}