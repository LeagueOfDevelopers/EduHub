using EduHubLibrary.Domain;
using EnsureThat;
using System;

namespace EduHubLibrary.Facades
{
    internal class ChatFacade : IChatFacade
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

        private readonly IGroupRepository _groupRepository;
    }
}