﻿using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.EventBus.EventTypes
{
    public class CourseFinishedEvent : EventInfoBase
    {
        public CourseFinishedEvent(string groupTitle, int groupId, string teacherName)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
            TeacherName = teacherName;
        }

        public string GroupTitle { get; }
        public int GroupId { get; }
        public string TeacherName { get; }

        public override EventType GetEventType()
        {
            return EventType.CourseFinished;
        }
    }
}