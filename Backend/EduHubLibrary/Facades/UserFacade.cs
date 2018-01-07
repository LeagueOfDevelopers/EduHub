using System;
using System.Collections.Generic;
using EduHubLibrary.Domain;
using EnsureThat;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Common;
using System.Linq;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Facades
{
    public class UserFacade : IUserFacade
    {
        public User GetUser(Guid id)
        {
            return _userRepository.GetUserById(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public Guid RegUser(string username, Credentials credentials, bool IsTeacher, TypeOfUser type, string avatarLink)
        {
            User user = new User(username, credentials, IsTeacher, type, avatarLink);
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

            if (suggestedRole == MemberRole.Teacher && _groupRepository.GetGroupById(groupId).Teacher != null)
            {
                throw new TeacherIsAlreadyFoundException();
            }
            else
            {
                Invitation newInvintation = new Invitation(inviterId, invitedId, groupId, suggestedRole, InvitationStatus.InProgress);
                invitedUser.AddInvitation(newInvintation);
            }
        }

        public IEnumerable<Invitation> GetAllInvitationsForUser(Guid userId)
        {
            Ensure.Guid.IsNotEmpty(userId);
            User currentUser = _userRepository.GetUserById(userId);
            return currentUser.GetAllInvitation();
        }

        public IEnumerable<GroupMembership> GetAllGroupsOfUser(Guid userId)
        {
            List<GroupMembership> groupsOfUser = new List<GroupMembership>();
            foreach (Group group in _groupRepository.GetAll())
            {
                if (group.GetAllMembers().Any(member => member.UserId==userId))
                {
                    groupsOfUser.Add(new GroupMembership(group, 
                        group.GetAllMembers().First(member => member.UserId == userId).MemberRole));
                }
            }
            return groupsOfUser;
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
