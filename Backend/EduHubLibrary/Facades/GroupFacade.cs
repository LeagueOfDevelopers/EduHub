using System;
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
            return group.Id;
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

        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly GroupSettings _groupSettings;
    }
}
