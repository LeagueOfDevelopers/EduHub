using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace EduHub.Models
{
    public class GroupResponse
    {
        public GroupResponse(GroupInfo groupInfo, CourseStatus status, User teacher, IEnumerable<Member> members, IUserFacade userFacade)
        {
            GroupInfo = groupInfo;
            Status = status;
            Educator = new Teacher(teacher);
            Members = new List<GroupMember>();

            foreach (Member member in members)
            {
                User currentMember = userFacade.GetUser(member.UserId);
                Members.Add(new GroupMember(member, currentMember.Name, currentMember.AvatarLink));
            }
        }

        public List<GroupMember> Members { get; set; }
        public GroupInfo GroupInfo { get; set; }
        public CourseStatus Status { get; set; }
        public Teacher Educator { get; set; }

        public struct GroupMember
        {
            public Guid UserId;
            public string Name;
            public string AvatarLink;
            public MemberRole MemberRole;
            public bool Paid;
            public bool AcceptedCourse;

            public GroupMember(Member member, string name, string avatarLink)
            {
                UserId = member.UserId;
                Name = name;
                AvatarLink = avatarLink;
                MemberRole = member.MemberRole;
                Paid = member.Paid;
                AcceptedCourse = member.AcceptedCurriculum;
            }
        }
        
        public struct Teacher
        {
            public Teacher(User teacher)
            {
                UserId = teacher.Id;
                Name = teacher.Name;
                AvatarLink = teacher.AvatarLink;
            }

            public Guid UserId;
            public string Name;
            public string AvatarLink;
        }
    }
}
