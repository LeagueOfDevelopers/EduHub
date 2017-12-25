﻿using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades
{
    public interface IGroupFacade
    {
        Guid CreateGroup(Guid userId, string title, List<string> tags, string description, int size, double totalValue);
        IEnumerable<Group> GetGroups();
        Group GetGroup(Guid id);
        IEnumerable<Member> GetMembersOfGroup(Guid groupId);
    }
}
