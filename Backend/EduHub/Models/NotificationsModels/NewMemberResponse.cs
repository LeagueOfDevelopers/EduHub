﻿using System;

namespace EduHub.Models.NotificationsModels
{
    public class NewMemberResponse
    {
        public NewMemberResponse(Guid groupId, string groupTitle, Guid userId, string username)
        {
            GroupId = groupId;
            GroupTitle = groupTitle;
            UserId = userId;
            Username = username;
        }

        public Guid GroupId { get; }
        public string GroupTitle { get; }
        public Guid UserId { get; }
        public string Username { get; }
    }
}