using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Facades.Views.GroupViews;

namespace EduHubLibrary.Facades
{
    public interface IChatFacade
    {
        int SendMessage(int senderId, int groupId, string text);
        MessageView GetMessage(int messageId, int groupId);
    }
}