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
            var currentGroup = _groupRepository.GetGroupById(groupId);
            using (var chat = new ChatSession(currentGroup))
            {
                chat.SendMessage(senderId, text);
            }
            _groupRepository.Update(currentGroup);
            currentGroup = _groupRepository.GetGroupById(groupId);
            return currentGroup.Messages.ToList().LastOrDefault(m => m.Text == text).Id;
        }

        public Message GetMessage(int messageId, int groupId)
        {
            return _groupRepository.GetGroupById(groupId).Messages.ToList().Find(m => m.Id.Equals(messageId));
        }
    }
}