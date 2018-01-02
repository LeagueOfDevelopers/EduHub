using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain;
using EduHubLibrary.Infrastructure;
using EnsureThat;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Common;

namespace EduHubLibrary.Facades
{
    public class UserFacade : IUserFacade
    {
        public User GetUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public Guid RegUser(string username, Credentials credentials, bool IsTeacher, Role role, string avatarLink)
        {
            User user = new User(username, credentials, IsTeacher, role, avatarLink);
            _userRepository.Add(user);
            return user.Id;
        }

        public User FindByCredentials(Credentials credentials)
        {

            return _userRepository.GetUserByCredentials(credentials);
        }

        public void ChangeStatusOfInvitation(Guid userId, Guid invitationId, InvitationStatus status)
        {
            Ensure.Guid.IsNotEmpty(userId);
            Ensure.Guid.IsNotEmpty(invitationId);
            User currentUser = _userRepository.GetUserById(userId);
            if (status.Equals(InvitationStatus.Accepted))
            {
                currentUser.AcceptInvitation(invitationId);
                Invitation currentInvitation = currentUser.GetInvitationById(invitationId);
                Group currentGroup = _groupRepository.GetGroupById(currentInvitation.GroupId);
                currentGroup.AddMember(currentInvitation.FromUser, currentInvitation.ToUser);
            }
            else if (status.Equals(InvitationStatus.Declined))
            {
                currentUser.DeclineInvitation(invitationId);
            }
            
        }

        public void Invite(Guid inviterId, Guid invitedId, Guid groupId)
        {
            Ensure.Guid.IsNotEmpty(inviterId);
            Ensure.Guid.IsNotEmpty(invitedId);
            Ensure.Guid.IsNotEmpty(groupId);
            Group currentGroup = _groupRepository.GetGroupById(groupId);
            Ensure.Bool.IsTrue(currentGroup.IsMember(inviterId), nameof(Invite),
                opt => opt.WithException(new NotEnoughPermissionsException(inviterId)));
            Ensure.Bool.IsFalse(currentGroup.IsMember(invitedId), nameof(Invite),
                opt => opt.WithException(new AlreadyMemberException(invitedId, groupId)));
            User invitedUser = _userRepository.GetUserById(invitedId);
            Invitation newInvintation = new Invitation(inviterId, invitedId, groupId, InvitationStatus.InProgress);
            invitedUser.AddInvitation(newInvintation);
        }

        public IEnumerable<Invitation> GetAllInvitationsForUser(Guid userId)
        {
            Ensure.Guid.IsNotEmpty(userId);
            User currentUser = _userRepository.GetUserById(userId);
            return currentUser.GetAllInvitation();
        }

        public UserFacade(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;

        }

        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;

    }
}
