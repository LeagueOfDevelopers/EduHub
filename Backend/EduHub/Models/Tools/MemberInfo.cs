using System;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;

namespace EduHub.Models.Tools
{
    public class MemberInfo
    {
        public MemberInfo(int userId, string name, string avatarLink, MemberRole role, bool paid,
            MemberCurriculumStatus curriculumStatus)
        {
            UserId = userId;
            Name = name;
            AvatarLink = avatarLink;
            Role = role;
            Paid = paid;
            CurriculumStatus = curriculumStatus;
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string AvatarLink { get; set; }
        public MemberRole Role { get; set; }
        public bool Paid { get; set; }
        public MemberCurriculumStatus CurriculumStatus { get; }
    }
}