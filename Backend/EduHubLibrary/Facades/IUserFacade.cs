using System;
using System.Collections.Generic;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Facades.Views;
using EduHubLibrary.Mailing;

namespace EduHubLibrary.Facades
{
    public interface IUserFacade
    {
        IEnumerable<User> GetUsers();
        User GetUser(Guid id);
        User FindByCredentials(Credentials credentials);
        void ChangeInvitationStatus(Guid userId, Guid invitationId, InvitationStatus status);
        void Invite(Guid inviterId, Guid invitedId, Guid groupId, MemberRole suggestedRole);
        IEnumerable<Invitation> GetAllInvitationsForUser(Guid userId);
        IEnumerable<Group> GetAllGroupsOfUser(Guid userId);
        IEnumerable<User> FindByName(string name);
        IEnumerable<UserInviteInfo> FindUsersForInvite(string name, Guid groupId);
        IEnumerable<string> GetNotifies(Guid userId);
        void AddNotify(Guid userId, string notify);
        void ChangePassword(Guid userId, string newPassword);
        void CheckAdminExistence(string email, string adminName, EmailSender emailSender);
    }
}