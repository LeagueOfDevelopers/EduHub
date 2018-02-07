using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduHubLibrary.Domain;
using EduHubLibrary.Settings;
using EnsureThat;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.Events;

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

        public void AddMember(Guid groupId, Guid newMemberId)
        {
            Ensure.Guid.IsNotEmpty(groupId);
            Ensure.Guid.IsNotEmpty(newMemberId);
            Ensure.Any.IsNotNull(_userRepository.GetUserById(newMemberId), nameof(newMemberId),
                opt => opt.WithException(new UserNotFoundException(newMemberId)));
            Group currentGroup = Ensure.Any.IsNotNull(_groupRepository.GetGroupById(groupId), nameof(AddMember),
                opt => opt.WithException(new GroupNotFoundException(groupId)));
            currentGroup.AddMember(newMemberId);
        }

        public void DeleteTeacher(Guid groupId, Guid requestedPerson, Guid teacherId)
        {
            Ensure.Guid.IsNotEmpty(groupId);
            Ensure.Guid.IsNotEmpty(requestedPerson);
            Ensure.Guid.IsNotEmpty(teacherId);
            Group currentGroup = Ensure.Any.IsNotNull(_groupRepository.GetGroupById(groupId), nameof(DeleteTeacher),
                opt => opt.WithException(new GroupNotFoundException(groupId)));
            Ensure.Any.IsNotNull(_userRepository.GetUserById(requestedPerson), nameof(DeleteTeacher),
                opt => opt.WithException(new UserNotFoundException(requestedPerson)));
            Ensure.Any.IsNotNull(_userRepository.GetUserById(teacherId), nameof(DeleteTeacher),
                 opt => opt.WithException(new UserNotFoundException(teacherId)));
            currentGroup.DeleteTeacher(requestedPerson, teacherId);
        }

        public void DeleteMember(Guid groupId, Guid requestedPerson, Guid deletingPerson)
        {
            Ensure.Guid.IsNotEmpty(requestedPerson);
            Ensure.Guid.IsNotEmpty(deletingPerson);
            Ensure.Guid.IsNotEmpty(groupId);
            Group currentGroup = Ensure.Any.IsNotNull(_groupRepository.GetGroupById(groupId), nameof(DeleteMember),
                opt => opt.WithException(new GroupNotFoundException(groupId)));
            Ensure.Any.IsNotNull(_userRepository.GetUserById(requestedPerson), nameof(DeleteMember),
                opt => opt.WithException(new UserNotFoundException(requestedPerson)));
            Ensure.Any.IsNotNull(_userRepository.GetUserById(deletingPerson), nameof(DeleteMember),
                opt => opt.WithException(new UserNotFoundException(deletingPerson)));
            currentGroup.DeleteMember(requestedPerson, deletingPerson);
        }

        public Group GetGroup(Guid id)
        {
            return _groupRepository.GetGroupById(id);
        }

        public IEnumerable<Group> FindByTags(IEnumerable<string> tags)
        {
            List<Group> result = new List<Group>();

            _groupRepository.GetAll().ToList().ForEach(g => 
            {
                if (g.DoesContainsTags(tags.ToList()))
                    result.Add(g);
            });

            result.Sort(delegate (Group group1, Group group2) 
            { return group1.GroupInfo.Tags.Count().CompareTo(group2.GroupInfo.Tags.Count()); });

            return result;
        }

        public IEnumerable<Member> GetGroupMembers(Guid id)
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

        //!!!
        public IEnumerable<Invitation> GetAllInvitations(Guid groupId)
        {
            return _groupRepository.GetGroupById(groupId).GetAllInvitation();
        }

        public void AddInvitation(Guid groupId, Invitation invitation)
        {
            _groupRepository.GetGroupById(groupId).AddInvitation(invitation);
        }
        //!!!

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

        public void AcceptCurriculum(Guid userId, Guid groupId)
        {
            Ensure.Guid.IsNotEmpty(userId);
            Ensure.Guid.IsNotEmpty(groupId);
            Ensure.Any.IsNotNull(_userRepository.GetUserById(userId), nameof(AcceptCurriculum),
                opt => opt.WithException(new UserNotFoundException(userId)));
            Group currentGroup = Ensure.Any.IsNotNull(_groupRepository.GetGroupById(groupId), nameof(AcceptCurriculum),
                opt => opt.WithException(new GroupNotFoundException(groupId)));
            currentGroup.AcceptCurriculum(userId);
        }

        public void OfferCurriculum(Guid userId, Guid groupId, string description)
        {
            Ensure.Guid.IsNotEmpty(userId);
            Ensure.Guid.IsNotEmpty(groupId);
            Ensure.Any.IsNotNull(_userRepository.GetUserById(userId), nameof(AcceptCurriculum),
                opt => opt.WithException(new UserNotFoundException(userId)));
            Group currentGroup = Ensure.Any.IsNotNull(_groupRepository.GetGroupById(groupId), nameof(AcceptCurriculum),
                opt => opt.WithException(new GroupNotFoundException(groupId)));
            currentGroup.OfferCurriculum(userId, description);
        }

        #region Editing of group

        public void ChangeGroupTitle(Guid idOfGroup, Guid idOfChanger, string newTitle)
        {
            Group currentGroup = _groupRepository.GetGroupById(idOfGroup);
            Member current = Ensure.Any.IsNotNull(currentGroup.GetMemberById(idOfChanger), nameof(currentGroup.GetMemberById),
                opt => opt.WithException(new MemberNotFoundException(idOfChanger)));
            Ensure.Bool.IsTrue(current.MemberRole == MemberRole.Creator, nameof(ChangeGroupTitle),
                opt => opt.WithException(new NotEnoughPermissionsException(idOfChanger)));
            Ensure.String.IsNotNullOrWhiteSpace(newTitle);
            currentGroup.GroupInfo.Title = newTitle;
        }

        public void ChangeGroupDescription(Guid idOfGroup, Guid idOfChanger, string newDescription)
        {
            Group currentGroup = _groupRepository.GetGroupById(idOfGroup);
            Member current = Ensure.Any.IsNotNull(currentGroup.GetMemberById(idOfChanger), nameof(currentGroup.GetMemberById),
                opt => opt.WithException(new MemberNotFoundException(idOfChanger)));
            Ensure.Bool.IsTrue(current.MemberRole == MemberRole.Creator, nameof(ChangeGroupDescription),
                opt => opt.WithException(new NotEnoughPermissionsException(idOfChanger)));
            currentGroup.GroupInfo.Description = newDescription;
        }

        public void ChangeGroupTags(Guid idOfGroup, Guid idOfChanger, List<string> newTags)
        {
            Group currentGroup = _groupRepository.GetGroupById(idOfGroup);
            Member current = Ensure.Any.IsNotNull(currentGroup.GetMemberById(idOfChanger), nameof(currentGroup.GetMemberById),
                opt => opt.WithException(new MemberNotFoundException(idOfChanger)));
            Ensure.Bool.IsTrue(current.MemberRole == MemberRole.Creator, nameof(ChangeGroupTags),
                opt => opt.WithException(new NotEnoughPermissionsException(idOfChanger)));
            currentGroup.GroupInfo.Tags = newTags;
        }

        public void ChangeGroupSize(Guid idOfGroup, Guid idOfChanger, int newSize)
        {
            Group currentGroup = _groupRepository.GetGroupById(idOfGroup);
            Member current = Ensure.Any.IsNotNull(currentGroup.GetMemberById(idOfChanger), nameof(currentGroup.GetMemberById),
                opt => opt.WithException(new MemberNotFoundException(idOfChanger)));
            Ensure.Bool.IsTrue(current.MemberRole == MemberRole.Creator, nameof(ChangeGroupSize),
                opt => opt.WithException(new NotEnoughPermissionsException(idOfChanger)));
            Ensure.Any.IsNotNull(newSize);
            Ensure.Bool.IsTrue(newSize <= _groupSettings.MaxGroupSize && newSize >= _groupSettings.MinGroupSize,
               nameof(CreateGroup), opt => opt.WithException(new ArgumentOutOfRangeException(nameof(newSize))));
            currentGroup.GroupInfo.Size = newSize;
        }

        public void ChangeGroupPrice(Guid idOfGroup, Guid idOfChanger, double newPrice)
        {
            Group currentGroup = _groupRepository.GetGroupById(idOfGroup);
            Member current = Ensure.Any.IsNotNull(currentGroup.GetMemberById(idOfChanger), nameof(currentGroup.GetMemberById),
                opt => opt.WithException(new MemberNotFoundException(idOfChanger)));
            Ensure.Bool.IsTrue(current.MemberRole == MemberRole.Creator, nameof(ChangeGroupPrice),
                opt => opt.WithException(new NotEnoughPermissionsException(idOfChanger)));
            Ensure.Any.IsNotNull(newPrice);
            Ensure.Bool.IsTrue(newPrice <= _groupSettings.MaxGroupValue && newPrice >= _groupSettings.MinGroupValue,
               nameof(CreateGroup), opt => opt.WithException(new ArgumentOutOfRangeException(nameof(newPrice))));
            currentGroup.GroupInfo.MoneyPerUser = newPrice;
        }

        #endregion

        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly GroupSettings _groupSettings;
        
    }
}
