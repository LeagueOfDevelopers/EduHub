using System;
using EduHubLibrary.Common;

namespace EduHubLibrary.Facades
{
    public interface IAuthUserFacade
    {
        Guid RegUser(string username, Credentials credentials, bool isTeacher, UserType userType);
        void ConfirmUser(Guid key);
    }
}