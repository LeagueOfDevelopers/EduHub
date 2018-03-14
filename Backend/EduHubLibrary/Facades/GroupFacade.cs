using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Facades.Views.GroupViews;
using EduHubLibrary.Settings;
using EnsureThat;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.Events;
using EduHubLibrary.Infrastructure;

namespace EduHubLibrary.Facades
{
    public class GroupFacade : IGroupFacade
    {
        private readonly IGroupRepository _groupRepository;
        private readonly GroupSettings _groupSettings;
        private readonly IUserRepository _userRepository;
        private readonly ISanctionRepository _sanctionRepository;
        private readonly IEventPublisher _publisher;

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
            CheckUserExistence(userId);
            var group = new Group(userId, title, tags, description, size, totalValue, isPrivate, groupType);
            _groupRepository.Add(group);

            tags.ForEach(tag => _publisher.PublishEvent(new UsingTagEvent(tag)));

            return group.GroupInfo.Id;
        }

        public void AddMember(int groupId, int newMemberId)
        {
            CheckUserExistence(newMemberId);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.AddMember(newMemberId);
        }

        public void DeleteTeacher(int groupId, int requestedPerson)
        {
            CheckUserExistence(requestedPerson);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.DeleteTeacher(requestedPerson);
        }

        public void DeleteMember(int groupId, int requestedPerson, int deletingPerson)
        {
            CheckUserExistence(requestedPerson);
            CheckUserExistence(deletingPerson);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.DeleteMember(requestedPerson, deletingPerson);
        }

        public FullGroupView GetGroup(int id)
        {
            var currentGroup = _groupRepository.GetGroupById(id);
            var members = currentGroup.Members;
            var memberAmount = currentGroup.Members.Count;
            var votersAmount = currentGroup.Members.FindAll(m => m.CurriculumStatus == MemberCurriculumStatus.Accepted).Count;
            var groupInfo = currentGroup.GroupInfo;
            var groupInfoView = new GroupInfoView(groupInfo.Id,
                groupInfo.Title, groupInfo.Size,
                memberAmount, groupInfo.Price, groupInfo.GroupType,
                groupInfo.Tags, groupInfo.Description, groupInfo.Curriculum,
                groupInfo.IsPrivate, groupInfo.IsActive,
                currentGroup.Status, votersAmount);

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
            }

            var messagesList = new List<MessageView>();

            currentGroup.Messages.ToList().ForEach(m => messagesList.Add(new MessageView(m.Id, m.SenderId,
                m.SentOn, m.Text)));

            var responseView = new FullGroupView(groupInfoView, membersInfo, messagesList);
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

        public IEnumerable<Member> GetGroupMembers(int id)
        {
            return _groupRepository.GetGroupById(id).Members;
        }

        public IEnumerable<Group> GetGroups()
        {
            return _groupRepository.GetAll();
        }

        public void AddInvitation(int groupId, Invitation invitation)
        {
            _groupRepository.GetGroupById(groupId).AddInvitation(invitation);
        }

        public void ApproveTeacher(int teacherId, int groupId)
        {
            var currentGroup = _groupRepository.GetGroupById(groupId);
            var teacher = _userRepository.GetUserById(teacherId);
            currentGroup.ApproveTeacher(teacher);
        }

        public void AcceptCurriculum(int userId, int groupId)
        {
            CheckUserExistence(userId);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.AcceptCurriculum(userId);
        }

        public void DeclineCurriculum(int userId, int groupId, string reason)
        {
            CheckUserExistence(userId);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.DeclineCurriculum(userId);

            using (var cs = new ChatSession(currentGroup))
            {
                cs.SendMessage(userId, reason);
            }
        }

        public void OfferCurriculum(int userId, int groupId, string description)
        {
            CheckUserExistence(userId);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.OfferCurriculum(userId, description);
        }

        public IEnumerable<Invitation> GetAllInvitations(int groupId)
        {
            return _groupRepository.GetGroupById(groupId).Invitations;
        }

        public void FinishCurriculum(int groupId, int userId)
        {
            CheckUserExistence(userId);
            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.FinishCurriculum(userId);
        }

        public void AddReview(int groupId, int userId, string title,
            string text)
        {
            Ensure.String.IsNotNullOrWhiteSpace(title);
            Ensure.String.IsNotNullOrWhiteSpace(text);
            CheckUserExistence(userId);

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
        }

        private void CheckUserExistence(int userId)
        {
            _userRepository.GetUserById(userId);
        }
    }
}