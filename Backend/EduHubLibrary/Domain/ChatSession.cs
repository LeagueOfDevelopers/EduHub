using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Domain.NotificationService.Notifications;

[assembly: InternalsVisibleTo("ChatTests")]

namespace EduHubLibrary.Domain
{
    public class ChatSession : IDisposable
    {
        private readonly Group _group;
        private readonly List<BaseMessage> _messages;

        public ChatSession(Group group)
        {
            _messages = new List<BaseMessage>();
            _group = group;
        }

        public void Dispose()
        {
            _group.CommitChatSession(_messages);
        }

        internal int SendUserMessage(int senderId, string text)
        {
            var message = new UserMessage(senderId, text);
            _messages.Add(message);

            return message.Id;
        }

        internal int SendGroupMessage(INotificationInfo notificationInfo)
        {
            var message = new GroupMessage(notificationInfo);
            _messages.Add(message);

            return message.Id;
        }
    }
}