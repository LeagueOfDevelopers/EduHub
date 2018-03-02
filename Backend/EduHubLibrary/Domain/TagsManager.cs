using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Domain
{
    public class TagsManager
    {
        public TagsManager()
        {
            Tags = new List<Tag>();
        }

        public IEnumerable<Tag> Tags { get; private set; }

        public IEnumerable<string> FindTag(string tag)
        {
            var foundTags = Tags.ToList().FindAll(t => t.Name.Contains(tag));

            foundTags.Sort((tag1, tag2) => { return tag2.Popularity.CompareTo(tag1.Popularity); });

            var result = new List<string>();
            foundTags.ForEach(t => result.Add(t.Name));

            return result;
        }

        internal void AddTag(string newTag)
        {
            var newTagsList = new List<Tag>(Tags);
            newTagsList.Add(new Tag(newTag));
            Tags = newTagsList;
        }

        internal void AddPopularity(string updatingTag)
        {
            Tags.ToList().Find(existingTag => existingTag.Name.Equals(updatingTag)).AddPopularity();
        }

        internal bool DoesExist(string tag)
        {
            return Tags.Any(t => t.Name == tag);
        }
    }
}