using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades
{
    public interface IGroupFacade
    {
        Guid CreateGroup(Guid userId, string title, List<string> tags, string description);
        IEnumerable<Group> GetGroups();
        Group GetGroup(Guid id);
    }
}
