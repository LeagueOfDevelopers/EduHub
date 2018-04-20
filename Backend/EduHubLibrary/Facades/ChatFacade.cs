using System.Linq;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;
using EnsureThat;
using EduHubLibrary.Facades.Views.GroupViews;

namespace EduHubLibrary.Facades
{
    public class ChatFacade : IChatFacade
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public ChatFacade(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
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

        public MessageView GetMessage(int messageId, int groupId)
        {
            var message = _groupRepository.GetGroupById(groupId).Messages.ToList().Find(m => m.Id.Equals(messageId));
            var sendername = _userRepository.GetUserById(message.SenderId).UserProfile.Name;
            return new MessageView(message.Id, message.SenderId, sendername, message.SentOn, message.Text);
        }
    }
}