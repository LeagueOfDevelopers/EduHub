using EduHubLibrary.Domain;
using EduHubLibrary.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class GroupsOfUserResponse
    {
        public GroupsOfUserResponse(IEnumerable<Group> groups, Guid userId, IGroupFacade groupFacade)
        {
            Groups = new List<GroupMembership>();

            foreach (Group group in groups)
            {
                GroupMembership membership = new GroupMembership(group, 
                    groupFacade.GetMembersOfGroup(group.GroupInfo.Id)
                    .First(member => member.UserId.Equals(userId)).MemberRole);

                Groups.Add(membership);
            }
        }

        public List<GroupMembership> Groups { get; private set; }

        public struct GroupMembership
        {
            public Group Group;
            public MemberRole Role;

            public GroupMembership(Group group, MemberRole role)
            {
                Group = group;
                Role = role;
            }
        }
    }
}
