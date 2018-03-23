using System;
using System.Collections.Generic;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Facades.Views.GroupViews;

namespace EduHubLibrary.Facades
{
    public interface IGroupFacade
    {
        int CreateGroup(int userId, string title, List<string> tags, string description,
            int size, double totalValue, bool isPrivate, GroupType groupType);

        void ApproveTeacher(int teacherId, int groupId);
        void AcceptCurriculum(int userId, int groupId);
        void DeclineCurriculum(int userId, int groupId, string reason);
        void OfferCurriculum(int userId, int groupId, string description);
        IEnumerable<Group> GetGroups();
        FullGroupView GetGroup(int id);
        IEnumerable<Group> FindByTags(IEnumerable<string> tags);
        IEnumerable<Group> FindGroup(string title, List<string> tags, GroupType type, double minPrice,
            double maxPrice, bool formed);
        IEnumerable<Member> GetGroupMembers(int groupId);
        void DeleteMember(int groupId, int requestedPerson, int requestingPerson);
        void AddMember(int groupId, int requestedPerson);
        void DeleteTeacher(int groupId, int requestedPerson);
        void AddInvitation(int groupId, Invitation invitation);
        void FinishCurriculum(int groupId, int userId);

        void AddReview(int groupId, int userId, string title,
            string text);

        IEnumerable<Invitation> GetAllInvitations(int groupId);
    }
}