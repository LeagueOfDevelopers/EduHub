﻿using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain;
using EduHubLibrary.Settings;
using EnsureThat;
using EduHubLibrary.Domain.Exceptions;

namespace EduHubLibrary.Facades
{
    public class GroupFacade : IGroupFacade
    {
        public GroupFacade(IGroupRepository groupRepository, IUserRepository userRepository,
            GroupSettings groupSettings)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _groupSettings = groupSettings;
        }

        public Guid CreateGroup(Guid userId, string title, List<string> tags, string description, int size, double totalValue, bool isPrivate,
            GroupType groupType)
        {
            Ensure.Bool.IsTrue(size <= _groupSettings.MaxGroupSize && size >= _groupSettings.MinGroupSize,
                nameof(CreateGroup), opt => opt.WithException(new ArgumentOutOfRangeException(nameof(size))));
            Ensure.Bool.IsTrue(totalValue <= _groupSettings.MaxGroupValue && totalValue >= _groupSettings.MinGroupValue,
                nameof(CreateGroup), opt => opt.WithException(new ArgumentOutOfRangeException(nameof(totalValue))));
            Ensure.Any.IsNotNull(_userRepository.GetUserById(userId), nameof(userId), 
                opt => opt.WithException(new UserNotFoundException(userId)));
            Group group = new Group(userId, title, tags, description, size, totalValue, isPrivate, groupType);
            _groupRepository.Add(group);
            return group.GroupInfo.Id;
        }

        public Group GetGroup(Guid id)
        {
            return _groupRepository.GetGroupById(id);
        }

        public IEnumerable<Member> GetAllMembersOfGroup(Guid id)
        {
            return _groupRepository.GetGroupById(id).GetAllMembers();
        }

        public IEnumerable<Group> GetGroups()
        {
            return _groupRepository.GetAll();
        }

        public IEnumerable<Member> GetMembersOfGroup(Guid groupId)
        {
            return _groupRepository.GetGroupById(groupId).GetAllMembers();
        }

        public void ApproveTeacher(Guid teacherId, Guid groupId)
        {
            Ensure.Guid.IsNotEmpty(teacherId);
            Ensure.Guid.IsNotEmpty(groupId);
            Ensure.Any.IsNotNull(_userRepository.GetUserById(teacherId), nameof(ApproveTeacher),
                opt => opt.WithException(new UserNotFoundException(teacherId)));
            Group currentGroup = Ensure.Any.IsNotNull(_groupRepository.GetGroupById(groupId), nameof(ApproveTeacher),
                opt => opt.WithException(new GroupNotFoundException(groupId)));

            User teacher = _userRepository.GetUserById(teacherId);
            currentGroup.ApproveTeacher(teacher);
        }

        public void AcceptCourse(Guid userId, Guid groupId)
        {
            Ensure.Guid.IsNotEmpty(userId);
            Ensure.Guid.IsNotEmpty(groupId);
            Ensure.Any.IsNotNull(_userRepository.GetUserById(userId), nameof(AcceptCourse),
                opt => opt.WithException(new UserNotFoundException(userId)));
            Group currentGroup = Ensure.Any.IsNotNull(_groupRepository.GetGroupById(groupId), nameof(AcceptCourse),
                opt => opt.WithException(new GroupNotFoundException(groupId)));
            currentGroup.AcceptCourse(userId);
        }

        public void OfferCourse(Guid userId, Guid groupId, string description)
        {
            Ensure.Guid.IsNotEmpty(userId);
            Ensure.Guid.IsNotEmpty(groupId);
            Ensure.Any.IsNotNull(_userRepository.GetUserById(userId), nameof(AcceptCourse),
                opt => opt.WithException(new UserNotFoundException(userId)));
            Group currentGroup = Ensure.Any.IsNotNull(_groupRepository.GetGroupById(groupId), nameof(AcceptCourse),
                opt => opt.WithException(new GroupNotFoundException(groupId)));
            Course course = new Course(description);
            currentGroup.OfferCourse(userId, course);
        }

        #region Editing of group

        public void ChangeTitleOfGroup(Guid idOfGroup, Guid idOfChanger, string newTitle)
        {
            Group currentGroup = _groupRepository.GetGroupById(idOfGroup);
            Member current = Ensure.Any.IsNotNull(currentGroup.GetMemberById(idOfChanger), nameof(currentGroup.GetMemberById),
                opt => opt.WithException(new MemberNotFoundException(idOfChanger)));
            Ensure.Bool.IsTrue(current.MemberRole == MemberRole.Creator, nameof(ChangeTitleOfGroup),
                opt => opt.WithException(new NotEnoughPermissionsException(idOfChanger)));
            Ensure.String.IsNotNullOrWhiteSpace(newTitle);
            currentGroup.GroupInfo.Title = newTitle;
        }

        public void ChangeDescriptionOfGroup(Guid idOfGroup, Guid idOfChanger, string newDescription)
        {
            Group currentGroup = _groupRepository.GetGroupById(idOfGroup);
            Member current = Ensure.Any.IsNotNull(currentGroup.GetMemberById(idOfChanger), nameof(currentGroup.GetMemberById),
                opt => opt.WithException(new MemberNotFoundException(idOfChanger)));
            Ensure.Bool.IsTrue(current.MemberRole == MemberRole.Creator, nameof(ChangeDescriptionOfGroup),
                opt => opt.WithException(new NotEnoughPermissionsException(idOfChanger)));
            currentGroup.GroupInfo.Description = newDescription;
        }

        public void ChangeTagsOfGroup(Guid idOfGroup, Guid idOfChanger, List<string> newTags)
        {
            Group currentGroup = _groupRepository.GetGroupById(idOfGroup);
            Member current = Ensure.Any.IsNotNull(currentGroup.GetMemberById(idOfChanger), nameof(currentGroup.GetMemberById),
                opt => opt.WithException(new MemberNotFoundException(idOfChanger)));
            Ensure.Bool.IsTrue(current.MemberRole == MemberRole.Creator, nameof(ChangeTagsOfGroup),
                opt => opt.WithException(new NotEnoughPermissionsException(idOfChanger)));
            currentGroup.GroupInfo.Tags = newTags;
        }

        public void ChangeSizeOfGroup(Guid idOfGroup, Guid idOfChanger, int newSize)
        {
            Group currentGroup = _groupRepository.GetGroupById(idOfGroup);
            Member current = Ensure.Any.IsNotNull(currentGroup.GetMemberById(idOfChanger), nameof(currentGroup.GetMemberById),
                opt => opt.WithException(new MemberNotFoundException(idOfChanger)));
            Ensure.Bool.IsTrue(current.MemberRole == MemberRole.Creator, nameof(ChangeSizeOfGroup),
                opt => opt.WithException(new NotEnoughPermissionsException(idOfChanger)));
            Ensure.Any.IsNotNull(newSize);
            currentGroup.GroupInfo.Size = newSize;
        }

        public void ChangePriceInGroup(Guid idOfGroup, Guid idOfChanger, double newPrice)
        {
            Group currentGroup = _groupRepository.GetGroupById(idOfGroup);
            Member current = Ensure.Any.IsNotNull(currentGroup.GetMemberById(idOfChanger), nameof(currentGroup.GetMemberById),
                opt => opt.WithException(new MemberNotFoundException(idOfChanger)));
            Ensure.Bool.IsTrue(current.MemberRole == MemberRole.Creator, nameof(ChangePriceInGroup),
                opt => opt.WithException(new NotEnoughPermissionsException(idOfChanger)));
            Ensure.Any.IsNotNull(newPrice);
            currentGroup.GroupInfo.MoneyPerUser = newPrice;
        }
        #endregion

        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly GroupSettings _groupSettings;
    }
}
