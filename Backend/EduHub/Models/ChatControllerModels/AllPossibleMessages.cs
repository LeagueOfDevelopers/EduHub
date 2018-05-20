namespace EduHub.Models.ChatControllerModels
{
    public class AllPossibleMessages
    {
        public AllPossibleMessages(UserMessageResponse userMessage, GroupMessageResponse groupMessage)
        {
            UserMessage = userMessage;
            GroupMessage = groupMessage;
        }

        public UserMessageResponse UserMessage { get; }
        public GroupMessageResponse GroupMessage { get; }
    }
}