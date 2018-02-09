using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.Tools
{
    public class MemberInfo
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string AvatarLink { get; set; }
        public MemberRole Role { get; set; }
        public bool Paid { get; set; }

        public MemberInfo(Guid userId, string name, string avatarLink, MemberRole role, bool paid)
        {
            UserId = userId;
            Name = name;
            AvatarLink = avatarLink;
            Role = role;
            Paid = paid;
        }
    }
}
