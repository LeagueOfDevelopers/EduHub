using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades
{
    public interface IGroupFacade
    {
        Guid CreateGroup(User creator);
        IEnumerable<Group> GetGroups();
        Group GetGroup(Guid id);
    }
}
