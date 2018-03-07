using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Facades.Views;
using EnsureThat;
using EduHubLibrary.Mailing;

namespace EduHubLibrary.Facades
{
    public class UserFacade : IUserFacade
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IKeysRepository _keysRepository;

        public UserFacade(IUserRepository userRepository, IGroupRepository groupRepository, IKeysRepository keysRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _keysRepository = keysRepository;
        }

        public User GetUser(Guid id)
        {
            return _userRepository.GetUserById(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public User FindByCredentials(Credentials credentials)
        {
            return _userRepository.GetUserByCredentials(credentials);
        }

        public void ChangeInvitationStatus(Guid userId, Guid invitationId, InvitationStatus status)
        {
            Ensure.Guid.IsNotEmpty(userId);
            Ensure.Guid.IsNotEmpty(invitationId);
            var currentUser = _userRepository.GetUserById(userId);
            if (status.Equals(InvitationStatus.Accepted))
            {
                currentUser.AcceptInvitation(invitationId);
                var currentInvitation = currentUser.GetInvitation(invitationId);
                if (currentInvitation.SuggestedRole == MemberRole.Member)
                {
                    var currentGroup = _groupRepository.GetGroupById(currentInvitation.GroupId);
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
            var currentGroup = _groupRepository.GetGroupById(groupId);
            var invitedUser = _userRepository.GetUserById(invitedId);

            Ensure.Bool.IsTrue(currentGroup.IsMember(inviterId), nameof(Invite),
                opt => opt.WithException(new NotEnoughPermissionsException(inviterId)));
            Ensure.Bool.IsFalse(currentGroup.IsMember(invitedId), nameof(Invite),
                opt => opt.WithException(new AlreadyMemberException(invitedId, groupId)));
            Ensure.Bool.IsFalse(invitedUser.Invitations.Any(c => c.GroupId == groupId),
                nameof(Invite), opt => opt.WithException(new AlreadyInvitedException(invitedId, groupId)));
            if (suggestedRole == MemberRole.Teacher)
                Ensure.Bool.IsTrue(invitedUser.UserProfile.IsTeacher, nameof(Invite),
                    opt => opt.WithException(new UserIsNotTeacher(invitedId)));
            Ensure.Bool.IsFalse(
                suggestedRole == MemberRole.Teacher && _groupRepository.GetGroupById(groupId).Teacher != null,
                nameof(Invite), opt => opt.WithException(new TeacherIsAlreadyFoundException()));
            var newInvintation =
                new Invitation(inviterId, invitedId, groupId, suggestedRole, InvitationStatus.InProgress);

            invitedUser.AddInvitation(newInvintation);
        }

        public IEnumerable<Invitation> GetAllInvitationsForUser(Guid userId)
        {
            Ensure.Guid.IsNotEmpty(userId);
            var currentUser = _userRepository.GetUserById(userId);
            return currentUser.Invitations;
        }

        public IEnumerable<Group> GetAllGroupsOfUser(Guid userId)
        {
            var groupsOfUser = new List<Group>();

            foreach (var group in _groupRepository.GetAll())
                if (group.Members.Any(member => member.UserId.Equals(userId)))
                    groupsOfUser.Add(group);

            return groupsOfUser;
        }

        public IEnumerable<User> FindByName(string name)
        {
            var result = _userRepository.GetAll().ToList().FindAll(u => u.UserProfile.Name.Contains(name));

            return result.OrderBy(u => u.UserProfile.Name.Length);
        }

        public IEnumerable<string> GetNotifies(Guid userId)
        {
            return _userRepository.GetUserById(userId).Notifies;
        }

        public void AddNotify(Guid userId, string notify)
        {
            _userRepository.GetUserById(userId).AddNotify(notify);
        }

        public IEnumerable<UserInviteInfo> FindUsersForInvite(string name, Guid groupId)
        {
            var currentGroup = _groupRepository.GetGroupById(groupId);
            var targets = _userRepository.GetAll().ToList()
                .FindAll(u => u.UserProfile.Name.Contains(name))
                .Where(u => !currentGroup.IsMember(u.Id));
            var result = new List<UserInviteInfo>();
            targets.ToList().ForEach(t => result.Add(
                new UserInviteInfo(
                    t.Invitations.Any(inv => inv.GroupId == groupId),
                    t.UserProfile.Name,
                    t.UserProfile.IsTeacher,
                    t.Id,
                    t.UserProfile.Email,
                    t.UserProfile.AvatarLink,
                    t.IsActive
                )));
            return result;
        }
    }
}