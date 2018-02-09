using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Common;
using EnsureThat;

namespace EduHubLibrary.Infrastructure
{
    public class InMemoryUserRepository : IUserRepository
    {
        public void Add(User user)
        {
            if (user == null)
                throw new ArgumentNullException();
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

        public User GetUserById(Guid userId)
        {
            Ensure.Guid.IsNotEmpty(userId);
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
            var currentUser = _listOfUsers.Find(current => current.Id == user.Id) ?? throw new UserNotFoundException(user.Id);
            currentUser = user;
        }

        public InMemoryUserRepository()
        {
            _listOfUsers = new List<User>();
        }

        private List<User> _listOfUsers;
    }
}
