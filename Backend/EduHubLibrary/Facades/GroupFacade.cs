﻿using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Facades
{
    public class GroupFacade : IGroupFacade
    {
        public GroupFacade(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public Guid CreateGroup(Guid userId, string title, List<string> tags, string description, int size, double totalValue)
        {
            Group group = new Group(userId, title, tags, description, size, totalValue);
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

        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
    }
}
