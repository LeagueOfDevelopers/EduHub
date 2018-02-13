using EduHubLibrary.Domain;
using System;

namespace EduHubLibrary.Facades
{
    internal class ChatFacade : IChatFacade
    {
        public void DeleteMessage(Guid messageId)
        {
            throw new NotImplementedException();
        }

        public void EditMessage(Guid messageId)
        {
            throw new NotImplementedException();
        }

        public Guid SendMessage(Guid senderId, Guid groupId, string text)
        {
            using (var chat = _groupRepository.GetGroupById(groupId).Chat)
            {
                return chat.SendMessage(senderId, groupId, text);
            }
        }

        private readonly IGroupRepository _groupRepository;

        public ChatFacade(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
    }
}