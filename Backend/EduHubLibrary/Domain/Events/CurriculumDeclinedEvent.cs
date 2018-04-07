﻿using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class CurriculumDeclinedEvent : EventInfoBase
    {
        public CurriculumDeclinedEvent(string groupTitle, int groupId, string declinedName)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
            DeclinedName = declinedName;
        }

        public string GroupTitle { get; }
        public int GroupId { get; }
        public string DeclinedName { get; }

        public override EventType GetEventType()
        {
            return EventType.CurriculumDeclined;
        }
    }
}