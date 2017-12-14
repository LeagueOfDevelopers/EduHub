using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduHubLibrary.Domain.Exceptions;

namespace EduHubLibrary.Infrastructure
{
    class InMemoryUserRepository : IUserRepository
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
            if (userId == null)
                throw new ArgumentNullException();
            return listOfUsers.FirstOrDefault(current => current.Id == userId);
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
