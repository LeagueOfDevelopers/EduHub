using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;

namespace EduHubLibrary.Facades
{
    public interface IUserFacade
    {
        Guid RegUser(string username, Credentials credentials, bool IsTeacher, UserType type, string avatarLink);
        IEnumerable<User> GetUsers();
        User GetUser(Guid id);
        User FindByCredentials(Credentials credentials);
        void ChangeInvitationStatus(Guid userId, Guid invitationId, InvitationStatus status);
        void Invite(Guid inviterId, Guid invitedId, Guid groupId, MemberRole suggestedRole);
        IEnumerable<Invitation> GetAllInvitationsForUser(Guid userId);
        IEnumerable<Group> GetAllGroupsOfUser(Guid userId);
        bool DoesUserExist(string name);
        IEnumerable<User> FindByName(string name);
        IEnumerable<Event> GetNotifies(Guid userId);
    }
}
