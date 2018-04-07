﻿using System;

namespace EduHub.Models
{
    public class MinUserForInvitationItem
    {
        public MinUserForInvitationItem(bool invited, string username, bool isTeacher, int id, string email,
            string avatarLink)
        {
            Invited = invited;
            Username = username;
            IsTeacher = isTeacher;
            Id = id;
            Email = email;
            AvatarLink = avatarLink;
        }

        public bool Invited { get; }
        public string Username { get; }
        public bool IsTeacher { get; }
        public int Id { get; }
        public string Email { get; }
        public string AvatarLink { get; }
    }
}