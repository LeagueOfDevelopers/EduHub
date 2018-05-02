using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Facades.Views.GroupViews;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Settings;
using EnsureThat;

namespace EduHubLibrary.Facades
{
    public class GroupFacade : IGroupFacade
    {
        private readonly IGroupRepository _groupRepository;
        private readonly GroupSettings _groupSettings;
        private readonly IEventPublisher _publisher;
        private readonly ISanctionRepository _sanctionRepository;
        private readonly IUserRepository _userRepository;

        public GroupFacade(IGroupRepository groupRepository, IUserRepository userRepository,
            ISanctionRepository sanctionRepository, GroupSettings groupSettings, IEventPublisher publisher)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _sanctionRepository = sanctionRepository;
            _groupSettings = groupSettings;
            _publisher = publisher;
        }


        public int CreateGroup(int userId, string title, List<string> tags, string description, int size,
            double totalValue, bool isPrivate,
            GroupType groupType)
        {
            Ensure.Bool.IsTrue(size <= _groupSettings.MaxGroupSize && size >= _groupSettings.MinGroupSize,
                nameof(CreateGroup), opt => opt.WithException(new ArgumentOutOfRangeException(nameof(size))));
            Ensure.Bool.IsTrue(totalValue <= _groupSettings.MaxGroupValue && totalValue >= _groupSettings.MinGroupValue,
                nameof(CreateGroup), opt => opt.WithException(new ArgumentOutOfRangeException(nameof(totalValue))));
            Ensure.Any.IsNotDefault(groupType, nameof(groupType), opt => opt.WithException(new ArgumentException()));
            CheckUserExistence(userId);
            var group = new Group(userId, title, tags, description, size, totalValue, isPrivate, groupType);
            tags.ForEach(tag => _publisher.PublishEvent(new UsingTagEvent(tag)));
            _groupRepository.Add(group);
            return group.GroupInfo.Id;
        }

        public void DeleteGroup(int groupId)
        {
            var currentGroup = _groupRepository.GetGroupById(groupId);
            _groupRepository.Delete(currentGroup);
        }

        public void AddMember(int groupId, int newMemberId)
        {
            CheckSanctions(newMemberId, groupId);
            var currentUser = _userRepository.GetUserById(newMemberId);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            Ensure.Bool.IsTrue(currentGroup.Teacher?.Id != newMemberId, nameof(currentUser),
                opt => opt.WithException(new AlreadyTeacherException(newMemberId)));
            Ensure.Bool.IsFalse(currentGroup.IsKicked(newMemberId), nameof(newMemberId),
                opt => opt.WithException(new NotEnoughPermissionsException(newMemberId)));
            currentGroup.AddMember(newMemberId);
            _groupRepository.Update(currentGroup);

            _publisher.PublishEvent(new NewMemberEvent(groupId, currentGroup.GroupInfo.Title, currentUser.UserProfile.Name));
            if (currentGroup.Members.Count == currentGroup.GroupInfo.Size)
                _publisher.PublishEvent(new GroupIsFormedEvent(currentGroup.GroupInfo.Title, groupId));
        }

        public void DeleteTeacher(int groupId, int requestedPerson)
        {
            CheckUserExistence(requestedPerson);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.DeleteTeacher(requestedPerson);
            _groupRepository.Update(currentGroup);
        }

        public void DeleteMember(int groupId, int requestedPerson, int deletingPerson)
        {
            CheckUserExistence(requestedPerson);
            CheckUserExistence(deletingPerson);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            var memberRole = currentGroup.GetMember(deletingPerson).MemberRole;
            
            currentGroup.DeleteMember(requestedPerson, deletingPerson);
            var deletingUser = _userRepository.GetUserById(deletingPerson);
            _groupRepository.Update(currentGroup);

            _publisher.PublishEvent(new MemberLeftEvent(groupId, currentGroup.GroupInfo.Title, deletingUser.UserProfile.Name));
            if (memberRole.Equals(MemberRole.Creator))
            {
                var newCreatorId = currentGroup.Members.Find(m => m.MemberRole.Equals(MemberRole.Creator)).UserId;
                var newCreator = _userRepository.GetUserById(newCreatorId);
                _publisher.PublishEvent(new NewCreatorEvent(groupId, currentGroup.GroupInfo.Title,
                    deletingUser.UserProfile.Name, newCreator.UserProfile.Name));
            }
        }

        public FullGroupView GetGroup(int id)
        {
            var currentGroup = _groupRepository.GetGroupById(id);
            var members = currentGroup.Members;
            var memberAmount = currentGroup.Members.Count;
            var votersAmount = currentGroup.Members.FindAll(m => m.CurriculumStatus == MemberCurriculumStatus.Accepted)
                .Count;
            var groupInfo = currentGroup.GroupInfo;
            var groupInfoView = new GroupInfoView(groupInfo.Id,
                groupInfo.Title, groupInfo.Size,
                memberAmount, groupInfo.Price, groupInfo.GroupType,
                groupInfo.Tags, groupInfo.Description, groupInfo.Curriculum,
                groupInfo.IsPrivate, groupInfo.IsActive,
                currentGroup.Status, votersAmount);
            var reviewView = new List<ReviewView>();

            var membersInfo = new List<GroupMemberInfoView>();
            members.ForEach(m =>
            {
                var currentMember = _userRepository.GetUserById(m.UserId);
                membersInfo.Add(new GroupMemberInfoView(m.UserId, currentMember.UserProfile.Name,
                    currentMember.UserProfile.AvatarLink, m.MemberRole, m.Paid, m.CurriculumStatus));
            });
            if (currentGroup.Teacher != null)
            {
                var teacher = _userRepository.GetUserById(currentGroup.Teacher.Id);
                membersInfo.Add(new GroupMemberInfoView(currentGroup.Teacher.Id,
                    teacher.UserProfile.Name, teacher.UserProfile.AvatarLink,
                    MemberRole.Teacher, false, MemberCurriculumStatus.Unknown));
                teacher.TeacherProfile?.Reviews.Where(r => r.FromGroup == id).ToList()
                    .ForEach(r => reviewView.Add(new ReviewView(r.FromUser, r.FromGroup,
                    r.Title, r.Text, r.Date)));
            }

            var responseView = new FullGroupView(groupInfoView, membersInfo, reviewView);
            return responseView;
        }

        public IEnumerable<Group> FindByTags(IEnumerable<string> tags)
        {
            var result = new List<Group>();

            _groupRepository.GetAll().ToList().ForEach(g =>
            {
                if (g.DoesContainsTags(tags.ToList()))
                    result.Add(g);
            });

            result.Sort((group1, group2) =>
            {
                return group1.GroupInfo.Tags.Count().CompareTo(group2.GroupInfo.Tags.Count());
            });

            return result;
        }

        public IEnumerable<Group> FindGroup(string title, List<string> tags = null,
            GroupType type = GroupType.Default, double minPrice = 0, double maxPrice = 0, bool formed = false)
        {
            var allGroups = _groupRepository.GetAll().ToList();
            allGroups = allGroups.Where(g => g.GroupInfo.Title.StartsWith(title))
                .OrderBy(g => g.GroupInfo.Title.Length).ToList();

            if (tags != null && tags.Any())
                allGroups = allGroups.FindAll(g => g.GroupInfo.Tags.Intersect(tags).Any())
                    .OrderByDescending(g => g.GroupInfo.Tags.Intersect(tags).Count()).ToList();

            if (type != GroupType.Default) allGroups = allGroups.FindAll(g => g.GroupInfo.GroupType == type);

            if (Math.Abs(minPrice) > 0 || Math.Abs(maxPrice) > 0)
                allGroups = allGroups.FindAll(g => minPrice <=
                                                   g.GroupInfo.Price && g.GroupInfo.Price <= maxPrice);

            if (formed) allGroups = allGroups.FindAll(g => g.GroupInfo.Size == g.Members.Count);

            return allGroups;
        }

        public IEnumerable<Member> GetGroupMembers(int id)
        {
            return _groupRepository.GetGroupById(id).Members;
        }

        public IEnumerable<MinGroupView> GetGroups()
        {
            var allGroups = _groupRepository.GetAll();
            allGroups = allGroups.Where(g => g.Status != CourseStatus.Finished && !g.GroupInfo.IsPrivate).ToList();
            var result = new List<MinGroupView>();
            allGroups.ToList().ForEach(g => result.Add(new MinGroupView(g.GroupInfo.Id, g.GroupInfo.Title, g.Members.Count, g.GroupInfo.Size,
                g.GroupInfo.Price, g.GroupInfo.GroupType, g.GroupInfo.Tags)));
            return result;
        }

        public void AddInvitation(int groupId, Invitation invitation)
        {
            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.AddInvitation(invitation);
            _groupRepository.Update(currentGroup);
        }

        public void ApproveTeacher(int teacherId, int groupId)
        {
            var currentGroup = _groupRepository.GetGroupById(groupId);
            var teacher = _userRepository.GetUserById(teacherId);
            Ensure.Bool.IsTrue(teacher.UserProfile.IsTeacher, nameof(teacher),
                opt => opt.WithException(new UserIsNotTeacher(teacherId)));
            Ensure.Bool.IsFalse(currentGroup.IsMember(teacherId), nameof(teacher),
                opt => opt.WithException(new AlreadyMemberException()));
            currentGroup.ApproveTeacher(teacher);
            _groupRepository.Update(currentGroup);

            _publisher.PublishEvent(new TeacherFoundEvent(teacher.UserProfile.Name, currentGroup.GroupInfo.Title, groupId));
        }

        public void AcceptCurriculum(int userId, int groupId)
        {
            CheckUserExistence(userId);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.AcceptCurriculum(userId);
            _groupRepository.Update(currentGroup);

            _publisher.PublishEvent(new CurriculumAcceptedEvent(currentGroup.GroupInfo.Title, groupId));
        }

        public void DeclineCurriculum(int userId, int groupId, string reason)
        {
            var currentUser = _userRepository.GetUserById(userId);
            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.DeclineCurriculum(userId);

            using (var cs = new ChatSession(currentGroup))
            {
                cs.SendMessage(userId, reason);
            }

            _groupRepository.Update(currentGroup);

            _publisher.PublishEvent(new CurriculumDeclinedEvent(currentGroup.GroupInfo.Title, groupId,
                currentUser.UserProfile.Name));
        }

        public void OfferCurriculum(int userId, int groupId, string description)
        {
            CheckUserExistence(userId);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.OfferCurriculum(userId, description);
            _groupRepository.Update(currentGroup);

            _publisher.PublishEvent(new CurriculumSuggestedEvent(description, currentGroup.GroupInfo.Title, groupId));
        }

        public IEnumerable<Invitation> GetAllInvitations(int groupId)
        {
            return _groupRepository.GetGroupById(groupId).Invitations;
        }

        public void FinishCurriculum(int groupId, int userId)
        {
            CheckUserExistence(userId);
            var currentGroup = _groupRepository.GetGroupById(groupId);
            var teacherId = currentGroup.Teacher.Id;
            var teacher = _userRepository.GetUserById(teacherId);
            currentGroup.FinishCurriculum(userId);
            _groupRepository.Update(currentGroup);

            _publisher.PublishEvent(new CourseFinishedEvent(currentGroup.GroupInfo.Title, groupId, teacher.UserProfile.Name));
        }

        public void AddReview(int groupId, int userId, string title, string text)
        {
            Ensure.String.IsNotNullOrWhiteSpace(title);
            Ensure.String.IsNotNullOrWhiteSpace(text);
            CheckUserExistence(userId);

            var currentUser = _userRepository.GetUserById(userId);
            var currentGroup = _groupRepository.GetGroupById(groupId);
            var teacher = _userRepository.GetUserById(currentGroup.Teacher.Id);

            Ensure.Bool.IsTrue(currentGroup.Status == CourseStatus.Finished, nameof(CourseStatus),
                opt => opt.WithException(new InvalidOperationException()));
            Ensure.Bool.IsTrue(currentGroup.IsMember(userId), nameof(userId),
                opt => opt.WithException(new NotEnoughPermissionsException(userId)));
            Ensure.Bool.IsTrue(teacher.TeacherProfile.Reviews.All(
                    r => r.FromGroup != currentGroup.GroupInfo.Id || r.FromUser != userId),
                nameof(teacher), opt => opt.WithException(new ReviewAlreadyAddedException(userId, teacher.Id)));

            teacher.TeacherProfile.AddReview(userId, title, text, groupId);
            _userRepository.Update(teacher);

            _publisher.PublishEvent(new ReviewReceivedEvent(currentGroup.GroupInfo.Title, groupId,
                currentUser.UserProfile.Name, title));
        }

        private void CheckUserExistence(int userId)
        {
            _userRepository.GetUserById(userId);
        }

        private void CheckSanctions(int userId, int groupId)
        {
            var doesSanctionAllowAction = _sanctionRepository.GetAllOfUser(userId).ToList()
                .Exists(s => s.IsActive && s.Type.Equals(SanctionType.NotAllowToJoinGroup));

            var hasUserInvitation = _userRepository.GetUserById(userId).Invitations.ToList()
                .Exists(i => i.GroupId == groupId);

            Ensure.Bool.IsFalse(doesSanctionAllowAction && !hasUserInvitation, nameof(CheckSanctions),
                opt => opt.WithException(new ActionIsNotAllowWithSanctionsException(SanctionType.NotAllowToJoinGroup)));
        }
    }
}