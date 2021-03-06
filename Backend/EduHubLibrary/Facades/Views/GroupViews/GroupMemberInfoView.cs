﻿using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Facades.Views.GroupViews
{
    public class GroupMemberInfoView
    {
        public GroupMemberInfoView(int userId, string username, string avatarLink,
            MemberRole memberRole, bool paid, MemberCurriculumStatus curriculumStatus)
        {
            UserId = userId;
            Username = username;
            AvatarLink = avatarLink;
            MemberRole = memberRole;
            Paid = paid;
            CurriculumStatus = curriculumStatus;
        }

        public int UserId { get; }
        public string Username { get; }
        public string AvatarLink { get; }
        public MemberRole MemberRole { get; }
        public bool Paid { get; }
        public MemberCurriculumStatus CurriculumStatus { get; }
    }
}