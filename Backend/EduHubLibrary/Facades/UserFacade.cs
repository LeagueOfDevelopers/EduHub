using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain;
using EduHubLibrary.Infrastructure;

namespace EduHubLibrary.Facades
{
    public class UserFacade : IUserFacade
    {
        public User GetUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public Guid RegUser(string username, string email,string password, bool IsTeacher)
        {
            User user = new User(username, email, password, IsTeacher);
            _userRepository.Add(user);
            return user.Id;
        }

        public UserFacade(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private readonly IUserRepository _userRepository;
    }
}
