using System;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Mailing;

namespace EduHubLibrary.Facades
{
    public interface IAccountFacade
    {
        Guid RegUser(string username, Credentials credentials, bool isTeacher);
        Guid RegUser(string username, Credentials credentials, bool isTeacher, Guid regKey);
        void ConfirmUser(Guid key);
        void ChangePassword(Guid userId, string newPassword);
        void ChangePassword(string newPassword, Guid key);
        void SendQueryToChangePassword(string email);
        void CheckAdminExistence(string email, string adminName);
    }
}