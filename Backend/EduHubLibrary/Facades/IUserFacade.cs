using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;

namespace EduHubLibrary.Facades
{
    public interface IUserFacade
    {
        Guid RegUser(string username, Credentials credentials, bool IsTeacher, TypeOfUser type, string avatarLink);
        IEnumerable<User> GetUsers();
        User GetUser(Guid id);
        User FindByCredentials(Credentials credentials);
        void ChangeStatusOfInvitation(Guid userId, Guid invitationId, InvitationStatus status);
        void Invite(Guid inviterId, Guid invitedId, Guid groupId, MemberRole suggestedRole);
        IEnumerable<Invitation> GetAllInvitationsForUser(Guid userId);
        IEnumerable<Group> GetAllGroupsOfUser(Guid userId);
        bool DoesUserExist(string name);
        User GetUserByName(string name);
    }
}
