﻿using System.Collections.Generic;
using EduHubLibrary.Common;

namespace EduHubLibrary.Domain
{
    public interface IUserRepository
    {
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        IEnumerable<User> GetAll();
        User GetUserById(int userId);
        User GetUserByEmail(string email);
        User GetUserByCredentials(Credentials credentials);
    }
}