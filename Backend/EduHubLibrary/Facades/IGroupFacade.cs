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
        void AcceptCurriculum(Guid userId, Guid groupId);
        void OfferCurriculum(Guid userId, Guid groupId, string description);
        IEnumerable<Group> GetGroups();
        Group GetGroup(Guid id);
        IEnumerable<Member> GetMembersOfGroup(Guid groupId);
        void ChangeTitleOfGroup(Guid idOfGroup, Guid idOfChanger, string newTitle);
        void ChangeDescriptionOfGroup(Guid idOfGroup, Guid idOfChanger, string newDescription);
        void ChangeTagsOfGroup(Guid idOfGroup, Guid idOfChanger, List<string> newTags);
        void ChangeSizeOfGroup(Guid idOfGroup, Guid idOfChanger, int newSize);
        void ChangePriceInGroup(Guid idOfGroup, Guid idOfChanger, double newPrice);
    }
}
