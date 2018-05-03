using System.Collections.Generic;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.NotificationService;
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
        IEnumerable<User> GetAllModerators(int callerId);

        IEnumerable<User> FindUser(string name, bool isTeacher, List<string> requiredTags, int minTeacherGroups,
            int minUserGroups);

        IEnumerable<User> FindByName(string name);
        IEnumerable<UserInviteInfo> FindUsersForInvite(string name, int groupId, bool isTeacher);
        IEnumerable<Notification> GetNotifies(int userId);

        void DemoteModerator(int moderatorId);
        IEnumerable<Event> GetModeratorsHistory();
    }
}