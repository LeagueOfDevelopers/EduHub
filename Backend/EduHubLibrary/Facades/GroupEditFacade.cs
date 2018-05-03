using System;
using System.Collections.Generic;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Settings;
using EnsureThat;
using EduHubLibrary.EventBus.EventTypes;

namespace EduHubLibrary.Facades
{
    public class GroupEditFacade : IGroupEditFacade
    {
        private readonly IGroupRepository _groupRepository;
        private readonly GroupSettings _groupSettings;
        private readonly IEventPublisher _publisher;

        public GroupEditFacade(IGroupRepository groupRepository, GroupSettings groupSettings, IEventPublisher publisher)
        {
            _groupRepository = groupRepository;
            _groupSettings = groupSettings;
            _publisher = publisher;
        }

        public void ChangeGroupTitle(int groupId, int changerId, string newTitle)
        {
            CheckMemberPermissions(changerId, groupId);
            CheckGroupStatus(groupId);
            Ensure.String.IsNotNullOrWhiteSpace(newTitle);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.GroupInfo.Title = newTitle;
            _groupRepository.Update(currentGroup);
        }

        public void ChangeGroupDescription(int groupId, int changerId, string newDescription)
        {
            CheckMemberPermissions(changerId, groupId);
            CheckGroupStatus(groupId);
            Ensure.String.IsNotNullOrWhiteSpace(newDescription);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.GroupInfo.Description = newDescription;
            _groupRepository.Update(currentGroup);
        }

        public void ChangeGroupTags(int groupId, int changerId, List<string> newTags)
        {
            CheckMemberPermissions(changerId, groupId);
            CheckGroupStatus(groupId);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.GroupInfo.Tags = newTags;
            _groupRepository.Update(currentGroup);

            newTags.ForEach(tag => _publisher.PublishEvent(new UsingTagEvent(tag)));
        }

        public void ChangeGroupSize(int groupId, int changerId, int newSize)
        {
            CheckMemberPermissions(changerId, groupId);
            CheckGroupStatus(groupId);
            Ensure.Any.IsNotNull(newSize);
            Ensure.Bool.IsTrue(newSize <= _groupSettings.MaxGroupSize && newSize >= _groupSettings.MinGroupSize,
                nameof(ChangeGroupSize), opt => opt.WithException(new ArgumentOutOfRangeException(nameof(newSize))));

            var currentGroup = _groupRepository.GetGroupById(groupId);
            Ensure.Bool.IsTrue(newSize >= currentGroup.Members.Count,
                nameof(ChangeGroupSize), opt => opt.WithException(new ArgumentOutOfRangeException(nameof(newSize))));

            currentGroup.GroupInfo.Size = newSize;
            currentGroup.CheckVoting();
            _groupRepository.Update(currentGroup);
        }

        public void ChangeGroupPrice(int groupId, int changerId, double newPrice)
        {
            CheckMemberPermissions(changerId, groupId);
            CheckGroupStatus(groupId);
            Ensure.Any.IsNotNull(newPrice);
            Ensure.Bool.IsTrue(newPrice <= _groupSettings.MaxGroupValue && newPrice >= _groupSettings.MinGroupValue,
                nameof(ChangeGroupPrice), opt => opt.WithException(new ArgumentOutOfRangeException(nameof(newPrice))));

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.GroupInfo.Price = newPrice;
            _groupRepository.Update(currentGroup);
        }

        public void ChangeGroupPrivacy(int groupId, int changerId, bool privacy)
        {
            CheckMemberPermissions(changerId, groupId);
            CheckGroupStatus(groupId);
            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.GroupInfo.IsPrivate = privacy;
            _groupRepository.Update(currentGroup);
        }

        public void ChangeGroupType(int groupId, int changerId, GroupType newType)
        {
            CheckMemberPermissions(changerId, groupId);
            CheckGroupStatus(groupId);
            Ensure.Any.IsNotDefault(newType, nameof(newType), opt => opt.WithException(new ArgumentException()));
            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.GroupInfo.GroupType = newType;
            _groupRepository.Update(currentGroup);
        }

        private void CheckMemberPermissions(int memberId, int groupId)
        {
            var currentGroup = _groupRepository.GetGroupById(groupId);
            var member = currentGroup.GetMember(memberId);
            Ensure.Bool.IsTrue(member.MemberRole == MemberRole.Creator, nameof(CheckMemberPermissions),
                opt => opt.WithException(new NotEnoughPermissionsException(memberId)));
        }

        private void CheckGroupStatus(int groupId)
        {
            var currentGroup = _groupRepository.GetGroupById(groupId);
            Ensure.Bool.IsTrue(currentGroup.Status == CourseStatus.Searching
                               || currentGroup.Status == CourseStatus.InProgress,
                nameof(CourseStatus), opt => opt.WithException(new InvalidOperationException()));
        }
    }
}