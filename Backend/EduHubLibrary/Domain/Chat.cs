using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.Tools;

[assembly: InternalsVisibleTo("ChatTests")]

namespace EduHubLibrary.Domain
{
    public class Chat : IDisposable
    {
        public Chat()
        {
            Messages = new List<Message>();
        }

        public IEnumerable<Message> Messages { get; private set; }

        public void Dispose()
        {
            ///!!!
        }

        internal Guid SendMessage(Guid senderId, Guid groupId, string text)
        {
            var message = new Message(senderId, groupId, text);
            var newMessageList = new List<Message>(Messages);
            newMessageList.Add(message);
            Messages = newMessageList;

            return message.Id;
        }

        internal void DeleteMessage(Guid messageId)
        {
            CheckMessageId(messageId);
            var newMessageList = new List<Message>(Messages);
            newMessageList.Remove(newMessageList.First(m => m.Id.Equals(messageId)));
            Messages = newMessageList;
        }

        internal void EditMessage(Guid messageId, string newText)
        {
            CheckMessageId(messageId);
            var newMessageList = new List<Message>(Messages);
            newMessageList.First(m => m.Id.Equals(messageId)).Text = newText;
            Messages = newMessageList;
        }

        internal Message GetMessage(Guid messageId)
        {
            CheckMessageId(messageId);
            return Messages.ToList().FirstOrDefault(m => m.Id.Equals(messageId));
        }

        private void CheckMessageId(Guid messageId)
        {
            if (!Messages.Any(m => m.Id.Equals(messageId))) throw new MessageNotFoundException(messageId);
        }
    }
}