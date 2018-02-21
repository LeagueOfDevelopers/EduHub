using System;
using System.Collections.Generic;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Facades
{
    public interface IGroupFacade
    {
        Guid CreateGroup(Guid userId, string title, List<string> tags, string description,
            int size, double totalValue, bool isPrivate, GroupType groupType);

        void ApproveTeacher(Guid teacherId, Guid groupId);
        void AcceptCurriculum(Guid userId, Guid groupId);
        void DeclineCurriculum(Guid userId, Guid groupId, string reason);
        void OfferCurriculum(Guid userId, Guid groupId, string description);
        IEnumerable<Group> GetGroups();
        Group GetGroup(Guid id);
        IEnumerable<Group> FindByTags(IEnumerable<string> tags);
        IEnumerable<Member> GetGroupMembers(Guid groupId);
        void DeleteMember(Guid groupId, Guid requestedPerson, Guid requestingPerson);
        void AddMember(Guid groupId, Guid requestedPerson);
        void DeleteTeacher(Guid groupId, Guid requestedPerson, Guid teacherId);
        void AddInvitation(Guid groupId, Invitation invitation);
        IEnumerable<Invitation> GetAllInvitations(Guid groupId);
    }
}