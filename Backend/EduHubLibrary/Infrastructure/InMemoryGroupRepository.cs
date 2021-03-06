﻿using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Interators;
using EnsureThat;

namespace EduHubLibrary.Infrastructure
{
    public class InMemoryGroupRepository : IGroupRepository
    {
        private readonly List<Group> _listOfGroups;

        public InMemoryGroupRepository()
        {
            _listOfGroups = new List<Group>();
        }

        public void Add(Group group)
        {
            Ensure.Any.IsNotNull(group);
            group.GroupInfo.Id = IntIterator.GetNextId();
            _listOfGroups.Add(group);
        }

        public void Delete(Group group)
        {
            Ensure.Any.IsNotNull(group);
            _listOfGroups.Remove(group);
        }

        public IEnumerable<Group> GetAll()
        {
            return _listOfGroups;
        }

        public Group GetGroupById(int id)
        {
            return Ensure.Any.IsNotNull(_listOfGroups.Find(current => current.GroupInfo.Id == id), nameof(GetGroupById),
                opt => opt.WithException(new GroupNotFoundException(id)));
        }

        public IEnumerable<Group> GetGroupsByMemberId(int memberId)
        {
            return _listOfGroups.FindAll(current => current.IsMember(memberId));
        }

        public void Update(Group group)
        {
            Ensure.Any.IsNotNull(group);
            var currentGroup = _listOfGroups.Find(current => current.GroupInfo.Id == group.GroupInfo.Id) ??
                               throw new GroupNotFoundException(group.GroupInfo.Id);
            currentGroup.Messages.ToList().ForEach(msg =>
            {
                if (msg.Id == 0) msg.Id = IntIterator.GetNextId();
            });
            currentGroup = group;
        }

        public IEnumerable<Group> GetGroupsByUserId(int userId)
        {
            var result = _listOfGroups.Where(g => 
            g.Members.Any(m => m.UserId == userId) || g.Teacher?.Id == userId).ToList();
            return result;
        }
    }
}