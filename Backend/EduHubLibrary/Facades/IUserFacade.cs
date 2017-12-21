using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades
{
    public interface IUserFacade
    {
        Guid RegUser(string username, string email, string password, bool IsTeacher);
        IEnumerable<User> GetUsers();
        User GetUser(Guid id);
    }
}
