using System;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Facades
{
    public interface IChatFacade
    {
        Guid SendMessage(Guid senderId, Guid groupId, string text);
        Message GetMessage(Guid messageId, Guid groupId);
    }
}