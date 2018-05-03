namespace EduHubLibrary.Mailing.MessageModels
{
    public class NewMemberMessage
    {
        public NewMemberMessage(string groupTitle, string userName, string receiverName)
        {
            GroupTitle = groupTitle;
            UserName = userName;
            ReceiverName = receiverName;
        }

        public string GroupTitle { get; }
        public string UserName { get; }
        public string ReceiverName { get; }
    }
}