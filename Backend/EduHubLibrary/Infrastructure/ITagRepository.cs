using EduHubLibrary.Domain.Tools;
using System;
using System.Collections.Generic;
using System.Text;

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
