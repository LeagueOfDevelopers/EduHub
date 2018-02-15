using EduHubLibrary.Domain.Tools;
using System;

namespace EduHubLibrary.Facades
{
    public interface IChatFacade
    {
        Guid SendMessage(Guid senderId, Guid groupId, string text);
        void EditMessage(Guid messageId, Guid groupId, string newText);
        void DeleteMessage(Guid messageId, Guid groupId);
        Message GetMessage(Guid messageId, Guid groupId);
    }
}