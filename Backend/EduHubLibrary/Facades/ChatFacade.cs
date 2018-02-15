using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;
using EnsureThat;
using System;
using System.Linq;

namespace EduHubLibrary.Facades
{
    public class ChatFacade : IChatFacade
    {
        public ChatFacade(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public Guid SendMessage(Guid senderId, Guid groupId, string text)
        {
            Ensure.Bool.IsTrue(!String.IsNullOrWhiteSpace(text), nameof(SendMessage),
                   opt => opt.WithException(new ArgumentException()));

            using (var chat = _groupRepository.GetGroupById(groupId).Chat)
            {
                return chat.SendMessage(senderId, groupId, text);
            }
        }

        public void EditMessage(Guid messageId, Guid groupId, string newText)
        {
            Ensure.Bool.IsTrue(!String.IsNullOrWhiteSpace(newText), nameof(EditMessage), 
                opt => opt.WithException(new ArgumentException()));

            using (var chat = _groupRepository.GetGroupById(groupId).Chat)
            {
                chat.EditMessage(messageId, newText);
            }
        }

        public void DeleteMessage(Guid messageId, Guid groupId)
        {
            using (var chat = _groupRepository.GetGroupById(groupId).Chat)
            {
                chat.DeleteMessage(messageId);
            }
        }

        public Message GetMessage(Guid messageId, Guid groupId)
        {
            return _groupRepository.GetGroupById(groupId).Chat.Messages.ToList().Find(m => m.Id.Equals(messageId));
        }

        private readonly IGroupRepository _groupRepository;
    }
}