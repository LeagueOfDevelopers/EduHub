using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain
{
    public interface ITagFacade
    {
        IEnumerable<string> FindTag(string tag);
        void UseTag(string tag);
    }
}
