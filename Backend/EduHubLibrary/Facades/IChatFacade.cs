using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Facades
{
    public interface IChatFacade
    {
        int SendMessage(int senderId, int groupId, string text);
        Message GetMessage(int messageId, int groupId);
    }
}