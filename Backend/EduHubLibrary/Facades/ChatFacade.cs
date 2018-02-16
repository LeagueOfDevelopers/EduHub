using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
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
            Ensure.String.IsNotNullOrWhiteSpace(text);

            using (var chat = _groupRepository.GetGroupById(groupId).Chat)
            {
                return chat.SendMessage(senderId, text);
            }
        }

        public void EditMessage(Guid userId, Guid messageId, Guid groupId, string newText)
        {
            CheckUserRights(userId, messageId, groupId);
            Ensure.String.IsNotNullOrWhiteSpace(newText);

            using (var chat = _groupRepository.GetGroupById(groupId).Chat)
            {
                chat.EditMessage(messageId, newText);
            }
        }

        public void DeleteMessage(Guid userId, Guid messageId, Guid groupId)
        {
            CheckUserRights(userId, messageId, groupId);
            using (var chat = _groupRepository.GetGroupById(groupId).Chat)
            {
                chat.DeleteMessage(messageId);
            }
        }

        public Message GetMessage(Guid messageId, Guid groupId)
        {
            return _groupRepository.GetGroupById(groupId).Chat.GetMessage(messageId);
        }

        private void CheckUserRights(Guid userId, Guid messageId, Guid groupId)
        {
            var senderId = GetMessage(messageId, groupId).SenderId;

            if (!senderId.Equals(userId)) throw new NotEnoughPermissionsException(userId);
        }

        private readonly IGroupRepository _groupRepository;
    }
}