using EduHubLibrary.Domain;
using EduHubLibrary.Domain.NotificationService;
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
        void AcceptCurriculum(Guid userId, Guid groupId);
        void OfferCurriculum(Guid userId, Guid groupId, string description);
        IEnumerable<Group> GetGroups();
        Group GetGroup(Guid id);
        IEnumerable<Group> FindByTags(IEnumerable<string> tags);
        IEnumerable<Member> GetGroupMembers(Guid groupId);
        void ChangeGroupTitle(Guid idOfGroup, Guid idOfChanger, string newTitle);
        void ChangeGroupDescription(Guid idOfGroup, Guid idOfChanger, string newDescription);
        void ChangeGroupTags(Guid idOfGroup, Guid idOfChanger, List<string> newTags);
        void ChangeGroupSize(Guid idOfGroup, Guid idOfChanger, int newSize);
        void ChangeGroupPrice(Guid idOfGroup, Guid idOfChanger, double newPrice);
        void DeleteMember(Guid groupId, Guid requestedPerson, Guid requestingPerson);
        void AddMember(Guid groupId, Guid requestedPerson);
        void DeleteTeacher(Guid groupId, Guid requestedPerson, Guid teacherId);
        void AddInvitation(Guid groupId, Invitation invitation);
    }
}
