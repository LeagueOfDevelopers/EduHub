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
            listOfUsers.Add(user);
        }

        public void Delete(User user)
        {
            if (user == null)
                throw new ArgumentNullException();
            listOfUsers.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return listOfUsers;
        }

        public User GetUserById(Guid userId)
        {
            Ensure.Guid.IsNotEmpty(userId);
            return Ensure.Any.IsNotNull(listOfUsers.Find(current => current.Id == userId), nameof(GetUserById),
               opt => opt.WithException(new UserNotFoundException(userId)));
        }

        public User GetUserByCredentials(Credentials credentials)
        {
            return listOfUsers.FirstOrDefault(current => current.Credentials == credentials);
        }

        public void Update(User user)
        {
            if (user == null)
                throw new ArgumentNullException();
            var currentUser = listOfUsers.Find(current => current.Id == user.Id) ?? throw new UserNotFoundException(user.Id);
            currentUser = user;
        }

        public InMemoryUserRepository()
        {
            listOfUsers = new List<User>();
        }

        List<User> listOfUsers;
    }
}
