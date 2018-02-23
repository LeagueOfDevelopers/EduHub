using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EduHubLibrary.Domain.Tools;

[assembly: InternalsVisibleTo("ChatTests")]

namespace EduHubLibrary.Domain
{
    public class ChatSession : IDisposable
    {
        private readonly Group _group;
        private List<Message> _messages;

        public ChatSession(Group group)
        {
            _messages = new List<Message>();
            _group = group;
        }

        public void Dispose()
        {
            _group.CommitChatSession(_messages);
        }

        internal Guid SendMessage(Guid senderId, string text)
        {
            var message = new Message(senderId, text);
            _messages.Add(message);

            return message.Id;
        }
    }
}