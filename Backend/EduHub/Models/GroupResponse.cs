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
        public GroupResponse(List<GroupMember> members, GroupInfo groupInfo, CourseStatus status, User educator)
        {
            Members = members;
            GroupInfo = groupInfo;
            Status = status;
            if (educator != null)
            {
                Educator = new Teacher(educator);
            }
        }

        public List<GroupMember> Members { get; set; }
        public GroupInfo GroupInfo { get; set; }
        public CourseStatus Status { get; set; }
        public Teacher Educator { get; set; }
        
        public struct Teacher
        {
            public Teacher(User user)
            {
                UserId = user.Id;
                Name = user.UserProfile.Name;
                AvatarLink = user.UserProfile.AvatarLink;
            }

            public Guid UserId;
            public string Name;
            public string AvatarLink;
        }
    }

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
}
