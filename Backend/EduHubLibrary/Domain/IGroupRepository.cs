using System;
using System.Collections.Generic;

namespace EduHubLibrary.Domain
{
    public interface IGroupRepository
    {
        void Add(Group group);
        void Delete(Group group);
        void Update(Group group);
        IEnumerable<Group> GetAll();
        Group GetGroupById(Guid id);
        IEnumerable<Group> GetGroupsByMemberId(Guid memberId);
    }
}