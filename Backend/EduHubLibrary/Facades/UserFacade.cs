using System;
using System.Collections.Generic;
using EduHubLibrary.Domain;
using EnsureThat;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Common;
using System.Linq;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.Events;

namespace EduHubLibrary.Facades
{
    public class UserFacade : IUserFacade
    {
        public UserFacade(IUserRepository userRepository, IGroupRepository groupRepository, IEventBus eventBus)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _eventBus = eventBus;
        }

        public User GetUser(Guid id)
        {
            return _userRepository.GetUserById(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public Guid RegUser(string username, Credentials credentials, bool IsTeacher, UserType type, string avatarLink)
        {
            Ensure.Bool.IsFalse(_userRepository.GetAll().Any(u => u.Credentials.Email.Equals(credentials.Email)),
                nameof(RegUser), opt => opt.WithException(new UserAlreadyExistsException(credentials.Email)));
            User user = new User(username, credentials, IsTeacher, type, avatarLink);
            _userRepository.Add(user);
            return user.Id;
        }

        public User FindByCredentials(Credentials credentials)
        {

            return _userRepository.GetUserByCredentials(credentials);
        }

        public void ChangeInvitationStatus(Guid userId, Guid invitationId, InvitationStatus status)
        {
            Ensure.Guid.IsNotEmpty(userId);
            Ensure.Guid.IsNotEmpty(invitationId);
            User currentUser = _userRepository.GetUserById(userId);
            if (status.Equals(InvitationStatus.Accepted))
            {
                currentUser.AcceptInvitation(invitationId);
                Invitation currentInvitation = currentUser.GetInvitationById(invitationId);
                if (currentInvitation.SuggestedRole == MemberRole.Member)
                {
                    Group currentGroup = _groupRepository.GetGroupById(currentInvitation.GroupId);
                    currentGroup.AddMember(currentInvitation.ToUser);
                }
            }
            else if (status.Equals(InvitationStatus.Declined))
            {
                currentUser.DeclineInvitation(invitationId);
            }
        }

        public void Invite(Guid inviterId, Guid invitedId, Guid groupId, MemberRole suggestedRole)
        {
            Ensure.Guid.IsNotEmpty(inviterId);
            Ensure.Guid.IsNotEmpty(invitedId);
            Ensure.Guid.IsNotEmpty(groupId);
            Group currentGroup = _groupRepository.GetGroupById(groupId);
            User invitedUser = _userRepository.GetUserById(invitedId);

            Ensure.Bool.IsTrue(currentGroup.IsMember(inviterId), nameof(Invite),
                opt => opt.WithException(new NotEnoughPermissionsException(inviterId)));
            Ensure.Bool.IsFalse(currentGroup.IsMember(invitedId), nameof(Invite),
                opt => opt.WithException(new AlreadyMemberException(invitedId, groupId)));
            Ensure.Bool.IsFalse(invitedUser.GetAllInvitation().Any(c => c.GroupId == groupId), 
                nameof(Invite), opt => opt.WithException(new AlreadyInvitedException(invitedId, groupId)));
            if (suggestedRole == MemberRole.Teacher)
            {
                Ensure.Bool.IsTrue(invitedUser.UserProfile.IsTeacher, nameof(Invite), 
                    opt => opt.WithException(new UserIsNotTeacher(invitedId)));
            }
            Ensure.Bool.IsFalse(suggestedRole == MemberRole.Teacher && _groupRepository.GetGroupById(groupId).Teacher != null,
                nameof(Invite), opt => opt.WithException(new TeacherIsAlreadyFoundException()));
            Invitation newInvintation = new Invitation(inviterId, invitedId, groupId, suggestedRole, InvitationStatus.InProgress);

            invitedUser.AddInvitation(newInvintation);
            _eventBus.SendMessage(new Event(new InvitationToGroupEvent(newInvintation)));
            _eventBus.Notify();
        }

        public IEnumerable<Invitation> GetAllInvitationsForUser(Guid userId)
        {
            Ensure.Guid.IsNotEmpty(userId);
            User currentUser = _userRepository.GetUserById(userId);
            return currentUser.GetAllInvitation();
        }

        public IEnumerable<Group> GetAllGroupsOfUser(Guid userId)
        {
            List<Group> groupsOfUser = new List<Group>();

            foreach (Group group in _groupRepository.GetAll())
            {
                if (group.GetAllMembers().Any(member => member.UserId.Equals(userId)))
                {
                    groupsOfUser.Add(group);
                }
            }

            return groupsOfUser;
        }

        public IEnumerable<User> FindByName(string name)
        {
            IEnumerable<User> result = _userRepository.GetAll().Where(u => u.UserProfile.Name.Contains(name));
            
            return result.OrderBy(u => u.UserProfile.Name.Length);
        }
     

        public bool DoesUserExist(string name)
        { 
            return _userRepository.GetAll().Any(user => user.UserProfile.Name.Contains(name));
        }

        public IEnumerable<Event> GetNotifies(Guid userId)
        {
            return _userRepository.GetUserById(userId).GetNotifies();
        }

        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private IEventBus _eventBus;
    }
}
