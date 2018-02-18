using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.Tools;

[assembly: InternalsVisibleTo("ChatTests")]

namespace EduHubLibrary.Domain
{
    public class ChatSession : IDisposable
    {
        public ChatSession(Group group)
        {
            Messages = new List<Message>();
            _group = group;
        }

        public IEnumerable<Message> Messages { get; private set; }
       
        public void Dispose()
        {
            _group.CommitChatSession(Messages);
        }

        internal Guid SendMessage(Guid senderId, string text)
        {
            var message = new Message(senderId, text);
            var newMessageList = new List<Message>(Messages);
            newMessageList.Add(message);
            Messages = newMessageList;

            return message.Id;
        }

        private Group _group;
    }
}