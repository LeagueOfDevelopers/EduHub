using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Facades.Views.GroupViews;
using EnsureThat;

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
            Ensure.Bool.IsTrue(HasRights(groupId, senderId), nameof(HasRights),
                opt => opt.WithException(new NotEnoughPermissionsException(senderId)));
            ;
            var currentGroup = _groupRepository.GetGroupById(groupId);
            using (var chat = new ChatSession(currentGroup))
            {
                chat.SendUserMessage(senderId, text);
            }
                    
            _groupRepository.Update(currentGroup);
            currentGroup = _groupRepository.GetGroupById(groupId);
            return currentGroup.Messages.ToList().LastOrDefault(m =>
            {
                var message = (UserMessage)m;
                return message.Text == text;
            }).Id;
        }

        public MessageView GetMessage(int messageId, int groupId, int userId)
        {
            Ensure.Bool.IsTrue(HasRights(groupId, userId), nameof(HasRights),
                opt => opt.WithException(new NotEnoughPermissionsException(userId)));

            var message = (UserMessage)_groupRepository.GetGroupById(groupId).Messages.ToList().Find(m => m.Id.Equals(messageId));
            var sendername = _userRepository.GetUserById(message.SenderId).UserProfile.Name;
            return new MessageView(message.Id, message.SenderId, sendername, message.SentOn, message.Text);
        }

        public IEnumerable<MessageView> GetMessagesForGroup(int groupId, int userId)
        {
            Ensure.Bool.IsTrue(HasRights(groupId, userId), nameof(HasRights),
                opt => opt.WithException(new NotEnoughPermissionsException(userId)));

            var currentGroup = _groupRepository.GetGroupById(groupId);

            var messagesList = new List<MessageView>();

            //TODO: return UserMessage and GroupMessage
            /*
            currentGroup.Messages.ToList().ForEach(m =>
            {
                var message = (UserMessage)m;
                messagesList.Add(new MessageView(message.Id, message.SenderId,
                  _userRepository.GetUserById(message.SenderId).UserProfile.Name, message.SentOn, message.Text));
            });
            */

            return messagesList;
        }

        private bool HasRights(int groupId, int userId)
        {
            var currentGroup = _groupRepository.GetGroupById(groupId);
            return currentGroup.IsTeacher(userId) || currentGroup.IsMember(userId);
        }
    }
}