using System.Collections.Generic;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Infrastructure
{
    public interface ITagRepository
    {
        IEnumerable<string> Find(string tag);
        void Add(string newTag);
        bool DoesExist(string tag);
        Tag Get(string tag);
        void Update(Tag tag);
    }
}