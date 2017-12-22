using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Facades
{
    public class GroupFacade : IGroupFacade
    {
        public GroupFacade(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public Guid CreateGroup(Guid userId)
        {
            Group group = new Group(userId);
            _groupRepository.Add(group);
            return group.Id;
        }

        public Group GetGroup(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetGroups()
        {
            return _groupRepository.GetAll();
        }

        private readonly IGroupRepository _groupRepository;
    }
}
