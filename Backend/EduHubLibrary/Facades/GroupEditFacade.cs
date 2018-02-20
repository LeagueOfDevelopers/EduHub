﻿using System;
using System.Collections.Generic;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Settings;
using EnsureThat;

namespace EduHubLibrary.Facades
{
    public class GroupEditFacade : IGroupEditFacade
    {
        private readonly IGroupRepository _groupRepository;
        private readonly GroupSettings _groupSettings;

        public GroupEditFacade(IGroupRepository groupRepository, GroupSettings groupSettings)
        {
            _groupRepository = groupRepository;
            _groupSettings = groupSettings;
        }

        public void ChangeGroupTitle(Guid groupId, Guid changerId, string newTitle)
        {
            CheckMemberPermissions(changerId, groupId);
            Ensure.String.IsNotNullOrWhiteSpace(newTitle);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.GroupInfo.Title = newTitle;
            _groupRepository.Update(currentGroup);
        }

        public void ChangeGroupDescription(Guid groupId, Guid changerId, string newDescription)
        {
            CheckMemberPermissions(changerId, groupId);
            Ensure.String.IsNotNullOrWhiteSpace(newDescription);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.GroupInfo.Description = newDescription;
            _groupRepository.Update(currentGroup);
        }

        public void ChangeGroupTags(Guid groupId, Guid changerId, List<string> newTags)
        {
            CheckMemberPermissions(changerId, groupId);

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.GroupInfo.Tags = newTags;
            _groupRepository.Update(currentGroup);
        }

        public void ChangeGroupSize(Guid groupId, Guid changerId, int newSize)
        {
            CheckMemberPermissions(changerId, groupId);
            Ensure.Any.IsNotNull(newSize);
            Ensure.Bool.IsTrue(newSize <= _groupSettings.MaxGroupSize && newSize >= _groupSettings.MinGroupSize,
                nameof(ChangeGroupSize), opt => opt.WithException(new ArgumentOutOfRangeException(nameof(newSize))));

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.GroupInfo.Size = newSize;
            _groupRepository.Update(currentGroup);
        }

        public void ChangeGroupPrice(Guid groupId, Guid changerId, double newPrice)
        {
            CheckMemberPermissions(changerId, groupId);
            Ensure.Any.IsNotNull(newPrice);
            Ensure.Bool.IsTrue(newPrice <= _groupSettings.MaxGroupValue && newPrice >= _groupSettings.MinGroupValue,
                nameof(ChangeGroupPrice), opt => opt.WithException(new ArgumentOutOfRangeException(nameof(newPrice))));

            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.GroupInfo.Price = newPrice;
            _groupRepository.Update(currentGroup);
        }

        public void ChangeGroupPrivacy(Guid groupId, Guid changerId, bool privacy)
        {
            CheckMemberPermissions(changerId, groupId);
            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.GroupInfo.IsPrivate = privacy;
            _groupRepository.Update(currentGroup);
        }

        public void ChangeGroupType(Guid groupId, Guid changerId, GroupType newType)
        {
            CheckMemberPermissions(changerId, groupId);
            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.GroupInfo.GroupType = newType;
            _groupRepository.Update(currentGroup);
        }

        private void CheckMemberPermissions(Guid memberId, Guid groupId)
        {
            var currentGroup = _groupRepository.GetGroupById(groupId);
            var member = currentGroup.GetMember(memberId);
            Ensure.Bool.IsTrue(member.MemberRole == MemberRole.Creator, nameof(CheckMemberPermissions),
                opt => opt.WithException(new NotEnoughPermissionsException(memberId)));
        }
    }
}