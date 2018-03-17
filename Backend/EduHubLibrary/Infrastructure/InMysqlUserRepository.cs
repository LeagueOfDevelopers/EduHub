﻿using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Data;
using EduHubLibrary.Data.UserDtos;
using EduHubLibrary.Domain;
using EduHubLibrary.Extensions;
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
            using(var _context = new EduhubContext(_connectionString)) { 
                var userDto = new UserDto();
                userDto.ParseFromUser(user);
                _context.Users.Add(userDto);
                _context.SaveChanges();
                user.Id = userDto.Id;
                _context.DetachAllEntities();
            }
        }

        public void Update(User user)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                _context.DetachAllEntities();
                var userDto = _context.Users
                    .Include(u => u.Contacts)
                    .Include(u => u.Invitations)
                    .Include(u => u.Reviews)
                    .Include("UserTags.Tag")
                    .Include(u => u.Notifies)
                    .FirstOrDefault(u => u.Id == user.Id);
                userDto.ParseFromUser(user);
                _context.SaveChanges();
            }
        }

        public void Delete(User user)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                _context.DetachAllEntities();
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
                _context.DetachAllEntities();
                var allUsers = new List<User>();
                var dtoList = _context.Users
                    .Include(u => u.Contacts)
                    .Include(u => u.Invitations)
                    .Include(u => u.Reviews)
                    .Include("UserTags.Tag")
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
                _context.DetachAllEntities();
                var userDto = _context.Users
                    .Include(u => u.Contacts)
                    .Include(u => u.Invitations)
                    .Include(u => u.Reviews)
                    .Include("UserTags.Tag")
                    .Include(u => u.Notifies)
                    .FirstOrDefault(u => u.Id == userId);
                var user = UserExtensions.ParseFromUserDto(userDto);
                return user;
            }
        }

        public User GetUserByEmail(string email)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                _context.DetachAllEntities();
                var userDto = _context.Users
                    .Include(u => u.Contacts)
                    .Include(u => u.Invitations)
                    .Include(u => u.Reviews)
                    .Include("UserTags.Tag")
                    .Include(u => u.Notifies)
                    .FirstOrDefault(u => u.Email == email);
                var user = UserExtensions.ParseFromUserDto(userDto);
                _context.SaveChanges();
                return user;
            }
        }

        public User GetUserByCredentials(Credentials credentials)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                _context.DetachAllEntities();
                var userDto = _context.Users
                    .Include(u => u.Contacts)
                    .Include(u => u.Invitations)
                    .Include("UserTags.Tag")
                    .Include(u => u.Reviews)
                    .Include(u => u.Notifies)
                    .FirstOrDefault(u => u.Email == credentials.Email
                                         && u.PasswordHash == credentials.PasswordHash);
                var user = UserExtensions.ParseFromUserDto(userDto);
                _context.SaveChanges();
                return user;
            }
        }
    }
}
