using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain
{
    public interface ITagsManager
    {
        IEnumerable<string> FindTag(string tag);
    }
}
