using EduHubLibrary.Common;

namespace EduHubLibrary.Facades
{
    public interface IAccountFacade
    {
        int RegUser(string username, Credentials credentials, bool isTeacher);
        int RegUser(string username, Credentials credentials, bool isTeacher, int regKey);
        void ConfirmUser(int key);
        void ChangePassword(int userId, string newPassword);
        void ChangePassword(string newPassword, int key);
        void SendQueryToChangePassword(string email);
        void CheckAdminExistence(string email);
        void SendTokenToModerator(string email);
    }
}