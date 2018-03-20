using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Data;
using EduHubLibrary.Data.GroupDtos;
using EduHubLibrary.Domain;
using EduHubLibrary.Extensions;
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

        public void Add(Group @group)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                _context.DetachAllEntities();
                var groupDto = new GroupDto();
                groupDto.ParseFromGroup(group);
                _context.Groups.Add(groupDto);
                _context.SaveChanges();
                group.GroupInfo.Id = groupDto.Id;
            }
        }

        public void Delete(Group @group)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                _context.DetachAllEntities();
                var currentGroupDto = _context.Groups.FirstOrDefault(g => g.Id == group.GroupInfo.Id);
                _context.Groups.Remove(currentGroupDto);
                _context.SaveChanges();
            }
        }

        public void Update(Group @group)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                _context.DetachAllEntities();
                var currentGroupDto = _context.Groups
                    .Include(g => g.Members)
                    .Include(g => g.Messages)
                    .Include(g => g.Invitations)
                    .Include(g => g.Tags)
                    .FirstOrDefault(g => g.Id == group.GroupInfo.Id);

                _context.RemoveRange(currentGroupDto.Tags);
                currentGroupDto.Tags.RemoveAll(t => true);
                _context.RemoveRange(currentGroupDto.Members);
                currentGroupDto.Members.RemoveAll(t => true);
                _context.RemoveRange(currentGroupDto.Messages);
                currentGroupDto.Messages.RemoveAll(t => true);

                currentGroupDto.ParseFromGroup(group);

                _context.SaveChanges();
            }
        }

        public IEnumerable<Group> GetAll()
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                _context.DetachAllEntities();
                var groups = _context.Groups.AsNoTracking()
                    .Include(g => g.Invitations)
                    .Include(g => g.Members)
                    .Include(g => g.Messages)
                    .Include(g => g.Tags)
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
                _context.DetachAllEntities();
                var currentGroupDto = _context.Groups
                    .Include(g => g.Invitations)
                    .Include(g => g.Messages)
                    .Include(g => g.Members)
                    .Include(g => g.Tags)
                    .FirstOrDefault(g => g.Id == id);
                var result = GroupExtensions.ParseFromGroupDto(currentGroupDto);
                return result;
            }
        }

        public IEnumerable<Group> GetGroupsByMemberId(int memberId)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                _context.DetachAllEntities();
                var foundValues = _context.Groups
                    .Include(g => g.Invitations)
                    .Include(g => g.Members)
                    .Include(g => g.Messages)
                    .Include(g => g.Tags)
                    .Where(g => g.Members.Any(m => m.Id == memberId))
                    .ToList();
                var result = new List<Group>();
                foundValues.ForEach(groupDto => result.Add(GroupExtensions.ParseFromGroupDto(groupDto)));
                return result;
            }
        }
    }
}
