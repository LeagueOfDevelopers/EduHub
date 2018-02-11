using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades
{
    public interface IChatFacade
    {
        Guid SendMessage(Guid senderId, Guid groupId, string text);
        void EditMessage(Guid messageId);
        void DeleteMessage(Guid messageId);
    }
}
