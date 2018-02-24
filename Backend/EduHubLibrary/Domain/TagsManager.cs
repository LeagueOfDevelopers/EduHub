using EduHubLibrary.Domain.Tools;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Tag> FindTag(string tag)
        {
            UpdatePopularity(tag);
            var result = Tags.ToList().FindAll(t => t.Name.Contains(tag));

            result.Sort((tag1, tag2) =>
            {
                return tag2.Popularity.CompareTo(tag1.Popularity);
            });

            return result;
        }

        private void UpdatePopularity(string updatingTag)
        {
            Tags.ToList().FindAll(t => t.Name.Contains(updatingTag)).ForEach(t => t.AddPopularity());
        }
    }
}