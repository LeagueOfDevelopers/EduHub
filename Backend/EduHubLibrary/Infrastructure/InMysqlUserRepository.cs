using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Data;
using EduHubLibrary.Data.UserDtos;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Extensions;
using EnsureThat;
using Microsoft.EntityFrameworkCore;

namespace EduHubLibrary.Infrastructure
{
    public class InMysqlUserRepository : IUserRepository
    {
        private readonly string _connectionString;


        public InMysqlUserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(User user)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var userDto = new UserDto();
                userDto.ParseFromUser(user);
                _context.Users.Add(userDto);
                _context.SaveChanges();
                user.Id = userDto.Id;
            }
        }

        public void Update(User user)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var userDto = _context.Users
                    .Include(u => u.Contacts)
                    .Include(u => u.Invitations)
                    .Include(u => u.Reviews)
                    .Include(u => u.Tags)
                    .Include(u => u.Notifies)
                    .FirstOrDefault(u => u.Id == user.Id);

                Ensure.Any.IsNotNull(userDto, nameof(user),
                    opt => opt.WithException(new UserNotFoundException(user.Id)));

                _context.RemoveRange(userDto.Tags);
                userDto.Tags.RemoveAll(t => true);
                _context.RemoveRange(userDto.Contacts);
                userDto.Contacts.RemoveAll(t => true);
                _context.RemoveRange(userDto.Reviews);
                userDto.Reviews.RemoveAll(t => true);
                _context.RemoveRange(userDto.Notifies);
                userDto.Notifies.RemoveAll(t => true);

                userDto.ParseFromUser(user);
                _context.SaveChanges();
            }
        }

        public void Delete(User user)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var userDto = new UserDto();
                userDto.ParseFromUser(user);
                _context.Users.Remove(userDto);
                _context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var allUsers = new List<User>();
                var dtoList = _context.Users
                    .Include(u => u.Contacts)
                    .Include(u => u.Invitations)
                    .Include(u => u.Tags)
                    .Include(u => u.Reviews)
                    .Include(u => u.Notifies)
                    .ToList();
                dtoList.ForEach(d => allUsers.Add(UserExtensions.ParseFromUserDto(d)));
                return allUsers;
            }
        }

        public User GetUserById(int userId)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var userDto = _context.Users
                    .Include(u => u.Contacts)
                    .Include(u => u.Invitations)
                    .Include(u => u.Tags)
                    .Include(u => u.Reviews)
                    .Include(u => u.Notifies)
                    .FirstOrDefault(u => u.Id == userId);
                Ensure.Any.IsNotNull(userDto, nameof(userId),
                    opt => opt.WithException(new UserNotFoundException(userId)));
                var user = UserExtensions.ParseFromUserDto(userDto);
                return user;
            }
        }

        public User GetUserByEmail(string email)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var userDto = _context.Users
                    .Include(u => u.Contacts)
                    .Include(u => u.Invitations)
                    .Include(u => u.Tags)
                    .Include(u => u.Reviews)
                    .Include(u => u.Notifies)
                    .FirstOrDefault(u => u.Email == email);
                Ensure.Any.IsNotNull(userDto, nameof(email),
                    opt => opt.WithException(new UserNotFoundException(email)));
                var user = UserExtensions.ParseFromUserDto(userDto);
                _context.SaveChanges();
                return user;
            }
        }

        public User GetUserByCredentials(Credentials credentials)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var userDto = _context.Users
                    .Include(u => u.Contacts)
                    .Include(u => u.Invitations)
                    .Include(u => u.Tags)
                    .Include(u => u.Reviews)
                    .Include(u => u.Notifies)
                    .FirstOrDefault(u => u.Email == credentials.Email
                                         && u.PasswordHash == credentials.PasswordHash);
                Ensure.Any.IsNotNull(userDto, nameof(credentials),
                    opt => opt.WithException(new UserNotFoundException()));

                var user = UserExtensions.ParseFromUserDto(userDto);
                _context.SaveChanges();
                return user;
            }
        }
    }
}