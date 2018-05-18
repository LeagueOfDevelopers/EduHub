using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Facades.Views.GroupViews;
using EnsureThat;
using EduHubLibrary.Domain.Message;

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

        public UserMessageView GetMessage(int messageId, int groupId, int userId)
        {
            Ensure.Bool.IsTrue(HasRights(groupId, userId), nameof(HasRights),
                opt => opt.WithException(new NotEnoughPermissionsException(userId)));

            var message = (UserMessage)_groupRepository.GetGroupById(groupId).Messages.ToList().Find(m => m.Id.Equals(messageId));
            var sendername = _userRepository.GetUserById(message.SenderId).UserProfile.Name;
            return new UserMessageView(message.SenderId, sendername, message.Text, message.Id, message.SentOn);
        }

        public IEnumerable<BaseMessageView> GetMessagesForGroup(int groupId, int userId)
        {
            Ensure.Bool.IsTrue(HasRights(groupId, userId), nameof(HasRights),
                opt => opt.WithException(new NotEnoughPermissionsException(userId)));

            var currentGroup = _groupRepository.GetGroupById(groupId);

            var messagesList = new List<BaseMessageView>();
            
            currentGroup.Messages.ToList().ForEach(m =>
            {
                if (m.GetMessageType().Equals(MessageType.UserMessage))
                {
                    var message = (UserMessage)m;
                    messagesList.Add(new UserMessageView(message.SenderId,
                      _userRepository.GetUserById(message.SenderId).UserProfile.Name, message.Text, message.Id, message.SentOn));
                }
                else if (m.GetMessageType().Equals(MessageType.GroupMessage))
                {
                    var message = (GroupMessage)m;
                    messagesList.Add(new GroupMessageView(message.NotificationInfo, message.NotificationType,
                        message.Id, message.SentOn));
                }
            });

            return messagesList;
        }

        private bool HasRights(int groupId, int userId)
        {
            var currentGroup = _groupRepository.GetGroupById(groupId);
            return currentGroup.IsTeacher(userId) || currentGroup.IsMember(userId);
        }
    }
}