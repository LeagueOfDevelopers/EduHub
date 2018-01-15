using EduHubLibrary.Domain;
using EduHubLibrary.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace EduHub.Models
{
    public class GroupResponse
    {
        public GroupResponse(GroupInfo groupInfo, Course course, User teacher,IEnumerable<Member> members, IUserFacade userFacade)
        {
            GroupInfo = groupInfo;
            Course = course;
            Teacher = teacher;
            Members = new List<GroupMember>();

            foreach (Member member in members)
            {
                User currentMember = userFacade.GetUser(member.UserId);
                Members.Add(new GroupMember(member, currentMember.Name, currentMember.AvatarLink));
            }
        }
        public List<GroupMember> Members { get; set; }
        public GroupInfo GroupInfo { get; set; }
        public Course Course { get; set; }
        public User Teacher { get; set; }

        public struct GroupMember
        {
            public Member Member;
            public string Name;
            public string AvatarLink;

            public GroupMember(Member member, string name, string avatarLink)
            {
                Member = member;
                Name = name;
                AvatarLink = avatarLink;
            }
        }
    }
}
