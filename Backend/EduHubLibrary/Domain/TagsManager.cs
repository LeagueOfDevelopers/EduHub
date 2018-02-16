using System.Collections.Generic;
using System.Linq;

namespace EduHubLibrary.Domain
{
    public class TagsManager
    {
        public TagsManager()
        {
            Tags = new List<string>();
        }

        public IEnumerable<string> Tags { get; private set; }

        public void AddTag(string newTag)
        {
            var newTagsList = new List<string>(Tags);
            newTagsList.Add(newTag);
            Tags = newTagsList;
        }

        public IEnumerable<string> FindTag(string tag)
        {
            return Tags.ToList().FindAll(t => t.Contains(tag));
        }
    }
}