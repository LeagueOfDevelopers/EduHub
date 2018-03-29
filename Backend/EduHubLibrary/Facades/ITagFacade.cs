using System.Collections.Generic;

namespace EduHubLibrary.Domain
{
    public interface ITagFacade
    {
        IEnumerable<string> FindTag(string tag);
        void UseTag(string tag);
    }
}