using System;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.Tools;
using EnsureThat;
using System.Linq;

namespace EduHubLibrary.Facades
{
    public class ChatFacade : IChatFacade
    {
        private readonly IGroupRepository _groupRepository;

        public ChatFacade(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public Guid SendMessage(Guid senderId, Guid groupId, string text)
        {
            Ensure.String.IsNotNullOrWhiteSpace(text);

            using (var chat = new ChatSession(_groupRepository.GetGroupById(groupId)))
            {
                return chat.SendMessage(senderId, text);
            }
        }
        
        public Message GetMessage(Guid messageId, Guid groupId)
        {
            return _groupRepository.GetGroupById(groupId).Messages.ToList().Find(m => m.Id.Equals(messageId));
        }
    }
}