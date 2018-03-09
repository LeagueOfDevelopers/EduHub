using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Interators;
using EnsureThat;

namespace EduHubLibrary.Infrastructure
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _listOfUsers;

        public InMemoryUserRepository()
        {
            _listOfUsers = new List<User>();
        }

        public void Add(User user)
        {
            if (user == null)
                throw new ArgumentNullException();
            user.Id = IntIterator.GetNextId();
            _listOfUsers.Add(user);
        }

        public void Delete(User user)
        {
            if (user == null)
                throw new ArgumentNullException();
            _listOfUsers.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _listOfUsers;
        }

        public User GetUserById(int userId)
        {
            return Ensure.Any.IsNotNull(_listOfUsers.Find(current => current.Id == userId), nameof(GetUserById),
                opt => opt.WithException(new UserNotFoundException(userId)));
        }

        public User GetUserByCredentials(Credentials credentials)
        {
            return _listOfUsers.FirstOrDefault(current => current.Credentials == credentials);
        }

        public void Update(User user)
        {
            if (user == null)
                throw new ArgumentNullException();
            var currentUser = _listOfUsers.Find(current => current.Id == user.Id) ??
                              throw new UserNotFoundException(user.Id);
            currentUser = user;
        }

        public User GetUserByEmail(string email)
        {
            return _listOfUsers.FirstOrDefault(current => current.Credentials.Email == email);
        }
    }
}