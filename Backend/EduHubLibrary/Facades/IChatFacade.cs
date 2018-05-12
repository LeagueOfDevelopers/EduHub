using System.Collections.Generic;
using EduHubLibrary.Facades.Views.GroupViews;

namespace EduHubLibrary.Facades
{
    public interface IChatFacade
    {
        int SendMessage(int senderId, int groupId, string text);
        UserMessageView GetMessage(int messageId, int groupId, int userId);
        IEnumerable<BaseMessageView> GetMessagesForGroup(int groupId, int userId);
    }
}