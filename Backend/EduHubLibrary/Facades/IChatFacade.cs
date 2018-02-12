using System;

namespace EduHubLibrary.Facades
{
    public interface IChatFacade
    {
        Guid SendMessage(Guid senderId, Guid groupId, string text);
        void EditMessage(Guid messageId);
        void DeleteMessage(Guid messageId);
    }
}