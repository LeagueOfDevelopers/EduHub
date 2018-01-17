using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Tools.Course
{
    public class Lesson
    {
        public LessonType LessonType { get; private set; }
        public string Description { get; private set; }
        public int Duration { get; private set; }
        public Homework Homework { get; private set; }
        public DateTime Date { get; private set; }
    }
}
