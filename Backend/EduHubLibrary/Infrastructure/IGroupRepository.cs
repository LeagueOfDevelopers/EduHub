using System.Collections.Generic;

namespace EduHubLibrary.Domain
{
    public interface IGroupRepository
    {
        void Add(Group group);
        void Delete(Group group);
        void Update(Group group);
        IEnumerable<Group> GetAll();
        Group GetGroupById(int id);
        IEnumerable<Group> GetGroupsByMemberId(int memberId);
        IEnumerable<Group> GetGroupsByUserId(int userId);
    }
}