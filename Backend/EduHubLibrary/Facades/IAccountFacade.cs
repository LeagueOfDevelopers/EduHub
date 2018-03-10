using System;
using EduHubLibrary.Common;

namespace EduHubLibrary.Facades
{
    public interface IAccountFacade
    {
        int RegUser(string username, Credentials credentials, bool isTeacher);
        int RegUser(string username, Credentials credentials, bool isTeacher, Guid regKey);
        void ConfirmUser(Guid key);
        void ChangePassword(int userId, string newPassword);
        void ChangePassword(string newPassword, Guid key);
        void SendQueryToChangePassword(string email);
        void CheckAdminExistence(string email);
    }
}