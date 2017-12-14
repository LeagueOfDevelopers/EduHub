using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Infrastructure
{
    public class InMemoryGroupRepository : IGroupRepository
    {

        public void Add(Group group)
        {
            if (group == null)
                throw new ArgumentNullException();
            listOfGroups.Add(group);
        }

        public void Delete(Group group)
        {
            if (group == null)
                throw new ArgumentNullException();
            listOfGroups.Remove(group);
        }

        public IEnumerable<Group> GetAll()
        {
            return listOfGroups;
        }

        public Group GetGroupById(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException();
            return listOfGroups.Find(current => current.Id == id);
        }

        public IEnumerable<Group> GetGroupsByMemberId(Guid memberId)
        {
            if (memberId == null)
                throw new ArgumentNullException();
            return listOfGroups.FindAll(current => current.IsMember(memberId));
        }

        public void Update(Group group)
        {
            if (group == null)
                throw new ArgumentNullException();
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
