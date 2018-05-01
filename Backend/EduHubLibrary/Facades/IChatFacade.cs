using System.Collections.Generic;
using EduHubLibrary.Facades.Views.GroupViews;

namespace EduHubLibrary.Facades
{
    public interface IChatFacade
    {
        int SendMessage(int senderId, int groupId, string text);
        MessageView GetMessage(int messageId, int groupId, int userId);
        IEnumerable<MessageView> GetMessagesForGroup(int groupId, int userId);
    }
}