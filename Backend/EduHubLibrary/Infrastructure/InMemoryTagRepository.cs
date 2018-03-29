using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Infrastructure
{
    public class InMemoryTagRepository : ITagRepository
    {
        private readonly List<Tag> _listOfTags;

        public InMemoryTagRepository()
        {
            _listOfTags = new List<Tag>();
        }

        public IEnumerable<string> Find(string tag)
        {
            var foundTags = _listOfTags.FindAll(t => t.Name.Contains(tag));

            foundTags.Sort((tag1, tag2) => { return tag2.Popularity.CompareTo(tag1.Popularity); });

            var result = new List<string>();
            foundTags.ForEach(t => result.Add(t.Name));

            return result;
        }

        public void Add(string newTag)
        {
            _listOfTags.Add(new Tag(newTag));
        }

        public bool DoesExist(string tag)
        {
            return _listOfTags.Any(t => t.Name == tag);
        }

        public Tag Get(string tag)
        {
            return _listOfTags.Find(t => t.Name == tag);
        }

        public void Update(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException();
            var currentTag = _listOfTags.Find(current => current.Name == tag.Name);
            currentTag = tag;
        }
    }
}