using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades
{
    public interface IUserFacade
    {
        Guid RegUser(string username, Credentials credentials, bool IsTeacher, Role role);
        IEnumerable<User> GetUsers();
        User GetUser(Guid id);
        User FindByCredentials(Credentials credentials);
        void ChangeStatusOfInvitation(Guid userId, Guid invitationId, InvitationStatus status);
        void Invite(Guid inviterId, Guid invitedId, Guid groupId);
        IEnumerable<Invitation> GetAllInvitationsForUser(Guid userId);
    }
}
