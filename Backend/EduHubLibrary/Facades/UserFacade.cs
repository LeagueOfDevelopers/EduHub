using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Facades.Views;
using EnsureThat;
using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.Facades
{
    public class UserFacade : IUserFacade
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IKeysRepository _keysRepository;
        private readonly IUserRepository _userRepository;

        public UserFacade(IUserRepository userRepository, IGroupRepository groupRepository,
            IKeysRepository keysRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _keysRepository = keysRepository;
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
            if (status.Equals(InvitationStatus.Accepted))
            {
                currentUser.AcceptInvitation(invitationId);
                var currentInvitation = currentUser.GetInvitation(invitationId);
                var currentGroup = _groupRepository.GetGroupById(currentInvitation.GroupId);
                if (currentInvitation.SuggestedRole == MemberRole.Member)
                {
                    Ensure.Bool.IsTrue(currentGroup.Teacher?.Id != userId, nameof(currentUser),
                        opt => opt.WithException(new AlreadyTeacherException(userId)));
                    currentGroup.AddMember(currentInvitation.ToUser);
                    _groupRepository.Update(currentGroup);
                }
                else if (currentInvitation.SuggestedRole == MemberRole.Teacher)
                {
                    Ensure.Bool.IsTrue(currentUser.UserProfile.IsTeacher, nameof(currentUser),
                        opt => opt.WithException(new UserIsNotTeacher(userId)));
                    Ensure.Bool.IsFalse(currentGroup.IsMember(userId), nameof(userId),
                        opt => opt.WithException(new AlreadyMemberException()));
                    currentGroup.ApproveTeacher(currentUser);
                    _groupRepository.Update(currentGroup);
                }
            }
            else if (status.Equals(InvitationStatus.Declined))
            {
                currentUser.DeclineInvitation(invitationId);
            }

            _userRepository.Update(currentUser);
        }

        public void Invite(int inviterId, int invitedId, int groupId, MemberRole suggestedRole)
        {
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
            _userRepository.Update(invitedUser);
            _groupRepository.Update(currentGroup);
        }

        public IEnumerable<Invitation> GetAllInvitationsForUser(int userId)
        {
            var currentUser = _userRepository.GetUserById(userId);
            return currentUser.Invitations;
        }

        public IEnumerable<Group> GetAllGroupsOfUser(int userId)
        {
            var groupsOfUser = new List<Group>();

            foreach (var group in _groupRepository.GetAll())
                if (group.Members.Any(member => member.UserId.Equals(userId)))
                    groupsOfUser.Add(group);

            return groupsOfUser;
        }

        public IEnumerable<User> FindUser(string name, bool isTeacher = false, List<string> requiredTags = null,
            int minTeacherGroups = 0, int minUserGroups = 0)
        {
            var allUsers = _userRepository.GetAll().ToList();
            var allGroups = _groupRepository.GetAll().ToList();
            allUsers = allUsers.Where(u => u.UserProfile.Name.StartsWith(name))
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
            var result = _userRepository.GetAll().ToList().FindAll(u => u.UserProfile.Name.Contains(name));

            return result.OrderBy(u => u.UserProfile.Name.Length);
        }

        public IEnumerable<string> GetNotifies(int userId)
        {
            return _userRepository.GetUserById(userId).Notifies;
        }

        public IEnumerable<UserInviteInfo> FindUsersForInvite(string name, int groupId)
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

        public void Report(int senderId, int suspectedId, string brokenRule)
        {
            //TODO: use RabbitMQ
        }
    }
}