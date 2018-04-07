﻿using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class MemberLeftEvent : EventInfoBase
    {
        public MemberLeftEvent(int groupId, string groupTitle, string username)
        {
            GroupId = groupId;
            GroupTitle = groupTitle;
            Username = username;
        }

        public int GroupId { get; }
        public string GroupTitle { get; }
        public string Username { get; }

        public override EventType GetEventType()
        {
            return EventType.MemberLeft;
        }
    }
}