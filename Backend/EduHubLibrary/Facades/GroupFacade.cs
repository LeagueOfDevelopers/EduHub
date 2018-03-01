using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Facades.Views.GroupViews;
using EduHubLibrary.Settings;
using EnsureThat;

namespace EduHubLibrary.Facades
{
    public class GroupFacade : IGroupFacade
    {
        private readonly IGroupRepository _groupRepository;
        private readonly GroupSettings _groupSettings;
        private readonly TagsManager _tagsManager;
        private readonly IUserRepository _userRepository;

        public GroupFacade(IGroupRepository groupRepository, IUserRepository userRepository,
            GroupSettings groupSettings, TagsManager tagsManager)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _groupSettings = groupSettings;
            _tagsManager = tagsManager;
        }


        public Guid CreateGroup(Guid userId, string title, List<string> tags, string description, int size,
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

            _tagsManager.UpdatePopularity(tags);

            return group.GroupInfo.Id;
        }

        public void AddMember(Guid groupId, Guid newMemberId)
        {
            Ensure.Guid.IsNotEmpty(groupId);
            Ensure.Guid.IsNotEmpty(newMemberId);
            CheckUserExistence(newMemberId);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.AddMember(newMemberId);
        }

        public void DeleteTeacher(Guid groupId, Guid requestedPerson)
        {
            Ensure.Guid.IsNotEmpty(groupId);
            Ensure.Guid.IsNotEmpty(requestedPerson);
            CheckUserExistence(requestedPerson);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.DeleteTeacher(requestedPerson);
        }

        public void DeleteMember(Guid groupId, Guid requestedPerson, Guid deletingPerson)
        {
            Ensure.Guid.IsNotEmpty(requestedPerson);
            Ensure.Guid.IsNotEmpty(deletingPerson);
            Ensure.Guid.IsNotEmpty(groupId);
            CheckUserExistence(requestedPerson);
            CheckUserExistence(deletingPerson);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.DeleteMember(requestedPerson, deletingPerson);
        }

        public FullGroupView GetGroup(Guid id)
        {
            var currentGroup = _groupRepository.GetGroupById(id);
            var members = currentGroup.Members;
            var memberAmount = currentGroup.Members.Count;
            var groupInfo = currentGroup.GroupInfo;
            var groupInfoView = new GroupInfoView(groupInfo.Id,
                groupInfo.Title, groupInfo.Size,
                memberAmount, groupInfo.Price, groupInfo.GroupType,
                groupInfo.Tags, groupInfo.Description, groupInfo.Curriculum,
                groupInfo.IsPrivate, groupInfo.IsActive,
                currentGroup.Status);

            var membersInfo = new List<GroupMemberInfo>();
            members.ForEach(m =>
            {
                var currentMember = _userRepository.GetUserById(m.UserId);
                membersInfo.Add(new GroupMemberInfo(m.UserId, currentMember.UserProfile.Name,
                    currentMember.UserProfile.AvatarLink, m.MemberRole, m.Paid, m.CurriculumStatus));
            });
            if (currentGroup.Teacher != null)
            {
                var teacher = _userRepository.GetUserById(currentGroup.Teacher.Id);
                membersInfo.Add(new GroupMemberInfo(currentGroup.Teacher.Id,
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

        public IEnumerable<Member> GetGroupMembers(Guid id)
        {
            return _groupRepository.GetGroupById(id).Members;
        }

        public IEnumerable<Group> GetGroups()
        {
            return _groupRepository.GetAll();
        }

        public void AddInvitation(Guid groupId, Invitation invitation)
        {
            _groupRepository.GetGroupById(groupId).AddInvitation(invitation);
        }

        public void ApproveTeacher(Guid teacherId, Guid groupId)
        {
            Ensure.Guid.IsNotEmpty(teacherId);
            Ensure.Guid.IsNotEmpty(groupId);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            var teacher = _userRepository.GetUserById(teacherId);
            currentGroup.ApproveTeacher(teacher);
        }

        public void AcceptCurriculum(Guid userId, Guid groupId)
        {
            Ensure.Guid.IsNotEmpty(userId);
            Ensure.Guid.IsNotEmpty(groupId);
            CheckUserExistence(userId);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.AcceptCurriculum(userId);
        }

        public void DeclineCurriculum(Guid userId, Guid groupId, string reason)
        {
            Ensure.Guid.IsNotEmpty(userId);
            Ensure.Guid.IsNotEmpty(groupId);
            CheckUserExistence(userId);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.DeclineCurriculum(userId);

            using (var cs = new ChatSession(currentGroup))
            {
                cs.SendMessage(userId, reason);
            }
        }

        public void OfferCurriculum(Guid userId, Guid groupId, string description)
        {
            Ensure.Guid.IsNotEmpty(userId);
            Ensure.Guid.IsNotEmpty(groupId);
            CheckUserExistence(userId);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.OfferCurriculum(userId, description);
        }

        public IEnumerable<Invitation> GetAllInvitations(Guid groupId)
        {
            return _groupRepository.GetGroupById(groupId).Invitations;
        }

        public void FinishCurriculum(Guid groupId, Guid userId)
        {
            Ensure.Guid.IsNotEmpty(groupId);
            Ensure.Guid.IsNotEmpty(userId);

            CheckUserExistence(userId);
            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.FinishCurriculum(userId);
        }

        public void AddReview(Guid groupId, Guid userId, string title,
            string text)
        {
            Ensure.Guid.IsNotEmpty(groupId);
            Ensure.Guid.IsNotEmpty(userId);
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

        private void CheckUserExistence(Guid userId)
        {
            _userRepository.GetUserById(userId);
        }
    }
}