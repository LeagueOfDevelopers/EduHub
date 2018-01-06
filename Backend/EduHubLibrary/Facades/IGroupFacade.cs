using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades
{
    public interface IGroupFacade
    {
        Guid CreateGroup(Guid userId, string title, List<string> tags, string description,
            int size, double totalValue, bool isPrivate, GroupType groupType);
        void ApproveTeacher(Guid teacherId, Guid groupId);
        void AcceptCourse(Guid userId, Guid groupId);
        void OfferCourse(Guid userId, Guid groupId, string description);
        IEnumerable<Group> GetGroups();
        Group GetGroup(Guid id);
        IEnumerable<Member> GetMembersOfGroup(Guid groupId);
    }
}
