using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EnsureThat;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Infrastructure
{
    public class InMemoryGroupRepository : IGroupRepository
    {

        public void Add(Group group)
        {
            Ensure.Any.IsNotNull(group);
            listOfGroups.Add(group);
        }

        public void Delete(Group group)
        {
            Ensure.Any.IsNotNull(group);
            listOfGroups.Remove(group);
        }

        public IEnumerable<Group> GetAll()
        {
            return listOfGroups;
        }

        public Group GetGroupById(Guid id)
        {
            Ensure.Guid.IsNotEmpty(id);
            return Ensure.Any.IsNotNull(listOfGroups.Find(current => current.Id == id), nameof(GetGroupById),
                opt => opt.WithException(new GroupNotFoundException(id)));
        }

        public IEnumerable<Group> GetGroupsByMemberId(Guid memberId)
        {
            Ensure.Guid.IsNotEmpty(memberId);
            return listOfGroups.FindAll(current => current.IsMember(memberId));
        }

        public void Update(Group group)
        {
            Ensure.Any.IsNotNull(group);
            var currentGroup = listOfGroups.Find(current => current.Id == group.Id) ?? throw new GroupNotFoundException(group.Id); 
            currentGroup = group;
        }

        public InMemoryGroupRepository()
        {
            listOfGroups = new List<Group>();
        }

        private List<Group> listOfGroups;

    }
}
