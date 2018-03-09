using System;
using System.Linq;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;
using EnsureThat;

namespace EduHubLibrary.Facades
{
    public class ChatFacade : IChatFacade
    {
        private readonly IGroupRepository _groupRepository;

        public ChatFacade(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public int SendMessage(int senderId, int groupId, string text)
        {
            Ensure.String.IsNotNullOrWhiteSpace(text);

            using (var chat = new ChatSession(_groupRepository.GetGroupById(groupId)))
            {
                return chat.SendMessage(senderId, text);
            }
        }

        public Message GetMessage(int messageId, int groupId)
        {
            return _groupRepository.GetGroupById(groupId).Messages.ToList().Find(m => m.Id.Equals(messageId));
        }
    }
}