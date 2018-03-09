using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EduHubLibrary.Common;
using EduHubLibrary.Data;
using EduHubLibrary.Data.UserDtos;
using EduHubLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace EduHubLibrary.Infrastructure
{
    public class InMysqlUserRepository : IUserRepository
    {

        private readonly EduhubContext _context;
        private readonly IMapper _mapper;


        public InMysqlUserRepository(EduhubContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(User user)
        {
            var userDto = _mapper.Map<User, UserDto>(user);
            _context.Users.Add(userDto);
            _context.SaveChanges();
            user.Id = userDto.Id;
            _context.Entry(userDto).State = EntityState.Detached;
        }

        public void Update(User user)
        {
            //var userDto = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            var userDto = _mapper.Map<User, UserDto>(user);
            _context.Update(userDto);
            _context.SaveChanges();
            _context.Entry(userDto).State = EntityState.Detached;

        }

        public void Delete(User user)
        {
            var userDto = _mapper.Map<User, UserDto>(user);
            _context.Users.Remove(userDto);
            _context.SaveChanges();
            _context.Entry(userDto).State = EntityState.Detached;
        }

        public IEnumerable<User> GetAll()
        {
            var allUsers = new List<User>();
            var dtoList = _context.Users.ToList();
            dtoList.ForEach(d => allUsers.Add(_mapper.Map<UserDto, User>(d)));
            return allUsers;
        }

        public User GetUserById(int userId)
        {
            var userDto = _context.Users.Find(userId);
            var user = _mapper.Map<UserDto, User>(userDto);
            _context.SaveChanges();
            _context.Entry(userDto).State = EntityState.Detached;
            return user;
        }

        public User GetUserByEmail(string email)
        {
            var userDto = _context.Users.FirstOrDefault(u => u.Email == email);
            var user = _mapper.Map<UserDto, User>(userDto);
            _context.SaveChanges();
            _context.Entry(userDto).State = EntityState.Detached;
            return user;
        }

        public User GetUserByCredentials(Credentials credentials)
        {
            var userDto = _context.Users.FirstOrDefault(u => u.Email == credentials.Email 
                                                             && u.PasswordHash == credentials.PasswordHash);
            var user = _mapper.Map<UserDto, User>(userDto);
            _context.SaveChanges();
            _context.Entry(userDto).State = EntityState.Detached;
            return user;
        }
    }
}
