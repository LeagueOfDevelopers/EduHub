using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Data;
using EduHubLibrary.Data.GroupDtos;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Extensions;
using EnsureThat;
using Microsoft.EntityFrameworkCore;

namespace EduHubLibrary.Infrastructure
{
    public class InMysqlGroupRepository : IGroupRepository
    {
        private readonly string _connectionString;

        public InMysqlGroupRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Group group)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var groupDto = new GroupDto();
                groupDto.ParseFromGroup(group);
                _context.Groups.Add(groupDto);
                _context.SaveChanges();
                group.GroupInfo.Id = groupDto.Id;
            }
        }

        public void Delete(Group group)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var currentGroupDto = _context.Groups.FirstOrDefault(g => g.Id == group.GroupInfo.Id);
                Ensure.Any.IsNotNull(currentGroupDto, nameof(currentGroupDto),
                    opt => opt.WithException(new GroupNotFoundException()));
                currentGroupDto.IsDeleted = true;
                _context.SaveChanges();
            }
        }

        public void Update(Group group)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var currentGroupDto = _context.Groups
                    .Include(g => g.Members)
                    .Include(g => g.Messages)
                    .Include(g => g.Invitations)
                    .Include(g => g.Kicked)
                    .Include(g => g.Tags)
                    .Include(g => g.GroupMessages)
                    .FirstOrDefault(g => g.Id == group.GroupInfo.Id && !g.IsDeleted);

                Ensure.Any.IsNotNull(currentGroupDto, nameof(currentGroupDto),
                    opt => opt.WithException(new GroupNotFoundException()));

                _context.RemoveRange(currentGroupDto.Tags);
                currentGroupDto.Tags.RemoveAll(t => true);
                _context.RemoveRange(currentGroupDto.Kicked);
                currentGroupDto.Kicked.RemoveAll(t => true);
                _context.RemoveRange(currentGroupDto.Members);
                currentGroupDto.Members.RemoveAll(t => true);

                currentGroupDto.ParseFromGroup(group);

                _context.SaveChanges();
            }
        }

        public IEnumerable<Group> GetAll()
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var groups = _context.Groups.AsNoTracking()
                    .Include(g => g.Invitations)
                    .Include(g => g.Members)
                    .Include(g => g.Messages)
                    .Include(g => g.Tags)
                    .Include(g => g.GroupMessages)
                    .Include(g => g.Kicked)
                    .Where(g => !g.IsDeleted)
                    .ToList();
                var allGroups = new List<Group>();
                groups.ForEach(g => allGroups.Add(GroupExtensions.ParseFromGroupDto(g)));
                return allGroups;
            }
        }

        public Group GetGroupById(int id)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var currentGroupDto = _context.Groups
                    .Include(g => g.Invitations)
                    .Include(g => g.Messages)
                    .Include(g => g.Members)
                    .Include(g => g.GroupMessages)
                    .Include(g => g.Kicked)
                    .Include(g => g.Tags)
                    .FirstOrDefault(g => g.Id == id && !g.IsDeleted);
                Ensure.Any.IsNotNull(currentGroupDto, nameof(currentGroupDto),
                    opt => opt.WithException(new GroupNotFoundException()));
                var result = GroupExtensions.ParseFromGroupDto(currentGroupDto);
                return result;
            }
        }

        public IEnumerable<Group> GetGroupsByMemberId(int memberId)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var foundValues = _context.Groups
                    .Include(g => g.Invitations)
                    .Include(g => g.Members)
                    .Include(g => g.Kicked)
                    .Include(g => g.Messages)
                    .Include(g => g.GroupMessages)
                    .Include(g => g.Tags)
                    .Where(g => g.Members.Any(m => m.Id == memberId) && !g.IsDeleted)
                    .ToList();
                var result = new List<Group>();
                foundValues.ForEach(groupDto => result.Add(GroupExtensions.ParseFromGroupDto(groupDto)));
                return result;
            }
        }
    }
}