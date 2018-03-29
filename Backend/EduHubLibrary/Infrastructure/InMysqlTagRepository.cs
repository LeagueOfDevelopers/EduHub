using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Data;
using EduHubLibrary.Data.TagDtos;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Extensions;

namespace EduHubLibrary.Infrastructure
{
    public class InMysqlTagRepository : ITagRepository
    {
        private readonly string _connectionString;

        public InMysqlTagRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<string> Find(string tag)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var foundTags = _context.Tags.ToList().FindAll(t => t.Name.Contains(tag));
                foundTags.Sort((tag1, tag2) => { return tag2.Popularity.CompareTo(tag1.Popularity); });

                var result = new List<string>();
                foundTags.ForEach(t => result.Add(t.Name));

                return result;
            }
        }

        public void Add(string newTag)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                _context.Tags.Add(new TagDto(newTag));
                _context.SaveChanges();
            }
        }

        public bool DoesExist(string tag)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                return _context.Tags.Any(t => t.Name == tag);
            }
        }

        public Tag Get(string tag)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var tagDto = _context.Tags.FirstOrDefault(t => t.Name == tag);
                return TagExtensions.ParseFromTagDto(tagDto);
            }
        }

        public void Update(Tag tag)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var tagDto = _context.Tags.FirstOrDefault(t => t.Name == tag.Name);
                tagDto.Popularity = tag.Popularity;
                _context.SaveChanges();
            }
        }
    }
}