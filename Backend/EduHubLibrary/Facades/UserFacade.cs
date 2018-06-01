using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.EventBus.EventTypes;
using EduHubLibrary.Facades.Views;
using EnsureThat;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace EduHubLibrary.Facades
{
    public class UserFacade : IUserFacade
    {
        private readonly IEventRepository _eventRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IEventPublisher _publisher;
        private readonly IUserRepository _userRepository;

        public UserFacade(IUserRepository userRepository, IGroupRepository groupRepository,
            IEventRepository eventRepository, IEventPublisher publisher)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _eventRepository = eventRepository;
            _publisher = publisher;
        }

        public User GetUser(int id)
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

        public void ChangeInvitationStatus(int userId, int invitationId, InvitationStatus status)
        {
            var currentUser = _userRepository.GetUserById(userId);
            var currentInvitation = currentUser.GetInvitation(invitationId);
            var currentGroup = _groupRepository.GetGroupById(currentInvitation.GroupId);

            if (status.Equals(InvitationStatus.Accepted))
            {
                currentUser.AcceptInvitation(invitationId);
                if (currentInvitation.SuggestedRole == MemberRole.Member)
                {
                    Ensure.Bool.IsTrue(currentGroup.Teacher?.Id != userId, nameof(currentUser),
                        opt => opt.WithException(new AlreadyTeacherException(userId)));
                    currentGroup.AddMember(currentInvitation.ToUser);
                    _groupRepository.Update(currentGroup);

                    _publisher.PublishEvent(new NewMemberEvent(currentGroup.GroupInfo.Id, currentGroup.GroupInfo.Title,
                        currentUser.UserProfile.Name));

                    if (currentGroup.Members.Count == currentGroup.GroupInfo.Size)
                        _publisher.PublishEvent(new GroupIsFormedEvent(currentGroup.GroupInfo.Title, currentGroup.GroupInfo.Id));
                }
                else if (currentInvitation.SuggestedRole == MemberRole.Teacher)
                {
                    Ensure.Bool.IsTrue(currentUser.UserProfile.IsTeacher, nameof(currentUser),
                        opt => opt.WithException(new UserIsNotTeacher(userId)));
                    Ensure.Bool.IsFalse(currentGroup.IsMember(userId), nameof(userId),
                        opt => opt.WithException(new AlreadyMemberException()));
                    currentGroup.ApproveTeacher(currentUser);
                    _groupRepository.Update(currentGroup);

                    _publisher.PublishEvent(new TeacherFoundEvent(currentUser.UserProfile.Name,
                        currentGroup.GroupInfo.Title, currentGroup.GroupInfo.Id));
                }

                _publisher.PublishEvent(new InvitationAcceptedEvent(currentGroup.GroupInfo.Title,
                    currentUser.UserProfile.Name,
                    currentInvitation.FromUser));
            }
            else if (status.Equals(InvitationStatus.Declined))
            {
                currentUser.DeclineInvitation(invitationId);
                _publisher.PublishEvent(new InvitationDeclinedEvent(currentGroup.GroupInfo.Title,
                    currentUser.UserProfile.Name,
                    currentInvitation.FromUser));
            }

            _userRepository.Update(currentUser);
        }

        public void Invite(int inviterId, int invitedId, int groupId, MemberRole suggestedRole)
        {
            var currentGroup = _groupRepository.GetGroupById(groupId);
            var invitedUser = _userRepository.GetUserById(invitedId);
            var inviterUser = _userRepository.GetUserById(inviterId);

            Ensure.Bool.IsTrue(currentGroup.Status == CourseStatus.Searching
                               || currentGroup.Status == CourseStatus.InProgress,
                nameof(CourseStatus), opt => opt.WithException(new InvalidOperationException()));
            Ensure.Bool.IsTrue(currentGroup.IsMember(inviterId), nameof(Invite),
                opt => opt.WithException(new NotEnoughPermissionsException(inviterId)));
            Ensure.Bool.IsFalse(currentGroup.IsMember(invitedId), nameof(Invite),
                opt => opt.WithException(new AlreadyMemberException(invitedId, groupId)));
            Ensure.Bool.IsFalse(invitedUser.Invitations.Any(c => c.GroupId == groupId
                                                                 && (c.Status == InvitationStatus.InProgress
                                                                     || c.Status == InvitationStatus.Declined)),
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
            _userRepository.Update(invitedUser);
            _groupRepository.Update(currentGroup);

            _publisher.PublishEvent(new InvitationReceivedEvent(currentGroup.GroupInfo.Title,
                inviterUser.UserProfile.Name,
                invitedId, suggestedRole));
        }

        public IEnumerable<InvitationView> GetAllInvitationsForUser(int userId)
        {
            var currentUser = _userRepository.GetUserById(userId);
            var invitations = currentUser.Invitations;
            var invitationViews = new List<InvitationView>();
            invitations.Where(i => i.Status == InvitationStatus.InProgress).ToList().ForEach(i =>
            {
                var currentGroup = _groupRepository.GetGroupById(i.GroupId);
                var inviter = _userRepository.GetUserById(i.FromUser);
                invitationViews.Add(new InvitationView(i.Id, i.FromUser, inviter.UserProfile.Name,
                    i.ToUser, currentUser.UserProfile.Name, i.GroupId, currentGroup.GroupInfo.Title,
                    i.SuggestedRole, i.Status));
            }
            );
            return invitationViews;
        }

        public IEnumerable<Group> GetAllGroupsOfUser(int userId)
        {
            var groupsOfUser = _groupRepository.GetGroupsByUserId(userId);

            return groupsOfUser;
        }

        public IEnumerable<User> FindUser(string name, bool isTeacher = false, List<string> requiredTags = null,
            int minTeacherGroups = 0, int minUserGroups = 0)
        {
            name = name.ToLower();
            var allUsers = _userRepository.GetAll().ToList();
            var allGroups = _groupRepository.GetAll().ToList();
            allUsers = allUsers.Where(u => u.UserProfile.Name.ToLower().Contains(name))
                .OrderBy(u => u.UserProfile.Name.Length).ToList();

            if (isTeacher) allUsers = allUsers.FindAll(u => u.UserProfile.IsTeacher);

            if (requiredTags != null && requiredTags.Any())
            {
                allUsers = allUsers.FindAll(u => u.TeacherProfile.Skills.Intersect(requiredTags).Any());
                allUsers = allUsers.OrderByDescending(u => u.TeacherProfile.Skills.Intersect(requiredTags).Count())
                    .ToList();
            }

            allUsers = allUsers.FindAll(u =>
                allGroups.Count(g => g.IsTeacher(u.Id) && g.Status == CourseStatus.Finished) >= minTeacherGroups);
            allUsers = allUsers.FindAll(u =>
                allGroups.Count(g => g.IsMember(u.Id) && g.Status == CourseStatus.Finished) >= minUserGroups);

            return allUsers;
        }

        public IEnumerable<User> FindByName(string name)
        {
            name = name.ToLower();
            var result = _userRepository.GetAll().ToList().FindAll(u => u.UserProfile.Name.ToLower().Contains(name));

            return result.OrderBy(u => u.UserProfile.Name.Length);
        }

        public IEnumerable<Notification> GetNotifies(int userId)
        {
            return _userRepository.GetUserById(userId).Notifications;
        }

        public IEnumerable<UserInviteInfo> FindUsersForInvite(string name, int groupId, bool isTeacher)
        {
            name = name.ToLower();
            var currentGroup = _groupRepository.GetGroupById(groupId);
            var targets = _userRepository.GetAll().ToList()
                .FindAll(u => u.UserProfile.Name.ToLower().Contains(name))
                .Where(u => !(currentGroup.IsMember(u.Id) || currentGroup.IsTeacher(u.Id)));

            if (isTeacher)
            {
                targets = targets.Where(u => u.UserProfile.IsTeacher == isTeacher);
            }

            var result = new List<UserInviteInfo>();
            targets.ToList().ForEach(t => result.Add(
                new UserInviteInfo(
                    t.Invitations.Any(inv => inv.GroupId == groupId),
                    t.UserProfile.Name,
                    t.UserProfile.IsTeacher,
                    t.Id,
                    t.UserProfile.AvatarLink,
                    t.IsActive
                )));
            return result;
        }

        public IEnumerable<User> GetAllModerators(int callerId)
        {
            return _userRepository.GetAll().Where(u =>
                (u.Type.Equals(UserType.Moderator) || u.Type.Equals(UserType.Admin)) && u.Id != callerId);
        }

        public void DemoteModerator(int moderatorId)
        {
            _userRepository.GetUserById(moderatorId).StopToBeModerator();
        }

        public IEnumerable<Event> GetModeratorsHistory()
        {
            return _eventRepository.GetModeratorsHistory();
        }
    }
}