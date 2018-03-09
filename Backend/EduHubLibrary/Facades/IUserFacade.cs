using System;
using System.Collections.Generic;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Facades.Views;

namespace EduHubLibrary.Facades
{
    public interface IUserFacade
    {
        IEnumerable<User> GetUsers();
        User GetUser(int id);
        User FindByCredentials(Credentials credentials);
        void ChangeInvitationStatus(int userId, int invitationId, InvitationStatus status);
        void Invite(int inviterId, int invitedId, int groupId, MemberRole suggestedRole);
        IEnumerable<Invitation> GetAllInvitationsForUser(int userId);
        IEnumerable<Group> GetAllGroupsOfUser(int userId);
        IEnumerable<User> FindByName(string name);
        IEnumerable<UserInviteInfo> FindUsersForInvite(string name, int groupId);
        IEnumerable<string> GetNotifies(int userId);
        void AddNotify(int userId, string notify);
    }
}