using System;
using EduHubLibrary.Common;

namespace EduHubLibrary.Facades
{
    public interface IAuthUserFacade
    {
        Guid RegUser(string username, Credentials credentials, bool isTeacher);
        Guid RegUser(string username, Credentials credentials, bool isTeacher, Guid regKey);
        void ConfirmUser(Guid key);
    }
}