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

        public void AddTag(string newTag)
        {
            var newTagsList = new List<Tag>(Tags);
            newTagsList.Add(new Tag(newTag));
            Tags = newTagsList;
        }

        public IEnumerable<string> FindTag(string tag)
        {
            var foundTags = Tags.ToList().FindAll(t => t.Name.Contains(tag));

            foundTags.Sort((tag1, tag2) => { return tag2.Popularity.CompareTo(tag1.Popularity); });

            var result = new List<string>();
            foundTags.ForEach(t => result.Add(t.Name));

            return result;
        }
        
        internal void UpdatePopularity(List<string> updatingTags)
        {
            updatingTags.ForEach(updatingTag => Tags.ToList().FindAll(
                existingTag => existingTag.Name.Contains(updatingTag)).
                ForEach(tag => tag.AddPopularity()));
        }
    }
}